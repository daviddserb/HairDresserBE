using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Commands.CreateAppointment
{
    // Implement IRequestHandler who was 2 parameters: the request command/query (must), and the return of the request (optional)
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.UserRepository.GetUserWithRoleByIdAsync(request.CustomerId);
            if (customer.Role != "customer") throw new NotFoundException($"Only customers can make appointments. The user '{customer.Username}' is not a customer!");

            const int limitCustomerAppointmentsPerMonth = 7;
            var customerAppointmentsLastMonth = await _unitOfWork.AppointmentRepository.CountCustomerAppointmentsLastMonthAsync(request.CustomerId);
            if (customerAppointmentsLastMonth >= limitCustomerAppointmentsPerMonth) throw new ClientException($"Customers can only make '{limitCustomerAppointmentsPerMonth}' appointments/month!");

            var employee = await _unitOfWork.UserRepository.GetUserWithRoleByIdAsync(request.EmployeeId);
            if (employee.Role != "employee") throw new NotFoundException($"Only employees can receive appointments. The user '{employee.Username}' is not a employee!");

            var hairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesByIdsAsync(request.HairServicesIds);
            if (hairServices == null) throw new NotFoundException($"Not all hair services ids '{String.Join(", ", request.HairServicesIds)}' are valid!");

            var employeeHairServicesIds = await _unitOfWork.UserRepository.GetEmployeeHairServicesIdsAsync(request.EmployeeId);
            bool employeeValidHairServices = request.HairServicesIds.All(id => employeeHairServicesIds.Contains(id));
            if (employeeValidHairServices == false) throw new ClientException($"The employee '{employee.Username}' is not valid for the selected hair services!");

            var priceForSelectedHairServices = await _unitOfWork.HairServiceRepository.GetPriceByHairServicesIdsAsync(request.HairServicesIds);
            if (request.Price != priceForSelectedHairServices) throw new ClientException($"The price '{request.Price}' is not valid for the selected hair services!");

            var durationForSelectedHairServices = await _unitOfWork.HairServiceRepository.GetDurationByHairServicesIdsAsync(request.HairServicesIds);
            TimeSpan timeDifference = request.EndDate - request.StartDate;
            if (durationForSelectedHairServices != timeDifference) throw new ClientException($"The duration '{timeDifference}' for the appointment is not valid for the selected hair services!");

            string dayOfWeek = request.StartDate.ToString("dddd");
            if (dayOfWeek == "Saturday" || dayOfWeek == "Sunday") throw new ClientException($"Can't create appointments for the weekend!");

            var workingDay = await _unitOfWork.WorkingDayRepository.GetWorkingDayByName(dayOfWeek);
            var employeeWorkingIntervals = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(request.EmployeeId, (int)workingDay);
            if (!employeeWorkingIntervals.Any()) throw new NotFoundException($"The employee '{employee.Username}' has no working intervals for ${dayOfWeek}!");

            TimeSpan appointmentStartTime = request.StartDate.TimeOfDay;
            TimeSpan appointmentEndTime = request.EndDate.TimeOfDay;
            bool atLeastOneIntervalValid = false;
            // Check if Appointment Interval is valid in the Employee's Working Interval.
            foreach (var interval in employeeWorkingIntervals)
            {
                // If valid
                if (appointmentStartTime >= interval.StartTime && appointmentEndTime <= interval.EndTime)
                {
                    atLeastOneIntervalValid = true;

                    var employeeAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsByEmployeeIdByDateAsync(request.EmployeeId, request.StartDate);
                    // Check if Appointment Interval is overlapping with the existing Employee's Appointments in that day.
                    foreach (var employeeAppointment in employeeAppointments)
                    {
                        bool appointmentsOverlap = appointmentStartTime < employeeAppointment.EndDate.TimeOfDay && appointmentEndTime > employeeAppointment.StartDate.TimeOfDay;
                        if (appointmentsOverlap == true) throw new ClientException($"The appointment interval is overlaping with the employee '{employee.Username}' current appointments!");
                    }

                    break; // Appointment Interval can be valid only in a singular Employee's Working Interval.
                }
            }
            if (atLeastOneIntervalValid == false)
            {
                // Create a list of formatted time intervals
                var formattedIntervals = employeeWorkingIntervals
                    .Select(interval => $"{interval.StartTime} - {interval.EndTime}")
                    .ToList();
                // Combine the formatted intervals into a comma-separated string
                var intervals = string.Join(", ", formattedIntervals);
                throw new ClientException($"The appointment's interval '{appointmentStartTime.ToString("hh\\:mm\\:ss")} - {appointmentEndTime.ToString("hh\\:mm\\:ss")}' is not available for the employee {employee.Username}'s working intervals: '{intervals}'");
            }

            // Check if Appointment Interval is overlapping with the existing Customer's Appointments in that day.
            var customerAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsByCustomerIdByDateAsync(request.CustomerId, request.StartDate);
            foreach (var customerAppointment in customerAppointments)
            {
                bool appointmentsOverlap = appointmentStartTime < customerAppointment.EndDate.TimeOfDay && appointmentEndTime > customerAppointment.StartDate.TimeOfDay;
                if (appointmentsOverlap == true) throw new ClientException($"The appointment interval is overlaping with the customer '{customer.Username}' current appointments!");
            }

            var appointment = new Appointment();
            appointment.CustomerId = customer.Id;
            appointment.EmployeeId = employee.Id;
            appointment.StartDate = request.StartDate;
            appointment.EndDate = request.EndDate;
            appointment.Price = request.Price;
            appointment.AppointmentHairServices = hairServices
                .Select(hairService => new AppointmentHairService()
                {
                    // Save only HairServiceId because AppointmentId still doesn't exist. It will exist only after the row is inserted in the Appointments table, but EFC will know how to make the link between the Appointment table (Id column) to the AppointmentsHairService table (AppointmentId column).
                    HairServiceId = hairService.Id
                })
                .ToList();

            await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);
            await _unitOfWork.SaveAsync();

            return await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(appointment.Id);
        }
    }
}
