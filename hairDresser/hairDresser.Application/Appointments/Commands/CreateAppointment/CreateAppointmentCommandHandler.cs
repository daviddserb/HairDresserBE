using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // !!! To add validation for everything.

            var appointment = new Appointment();

            var customer = await _unitOfWork.UserRepository.GetUserWithRoleByIdAsync(request.CustomerId);
            if (customer.Role != "customer") throw new NotFoundException($"Only customers can make appointments. The user '{customer.Username}' is not a customer.");

            var employee = await _unitOfWork.UserRepository.GetUserWithRoleByIdAsync(request.EmployeeId);
            if (employee.Role != "employee") throw new NotFoundException($"Only employees can receive appointments. The user '{customer.Username}' is not a employee.");

            var hairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesByIdsAsync(request.HairServicesIds);
            if (hairServices == null) throw new NotFoundException($"Not all hair services ids '{String.Join(", ", request.HairServicesIds)}' are valid!");

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
