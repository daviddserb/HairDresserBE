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
    // Trebuie sa implementam interfata IRequestHandler, care poate sa primeasca 2 parametrii: request-ul (mesajul = command/query) care este obligatoriu si raspunsul mesajului (ce returneaza el).
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("CreateAppointmentCommandHandler");
            var appointment = new Appointment();

            var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.CustomerId);
            if (customer == null) throw new NotFoundException($"The customer with the id '{request.CustomerId}' does not exist!");

            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
            if (employee == null) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' does not exist!");

            var hairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesByIdsAsync(request.HairServicesIds);
            if (hairServices == null) throw new NotFoundException($"There is a problem with the hair services ids: '{String.Join(", ", request.HairServicesIds)}'!");

            appointment.CustomerId = customer.Id;
            appointment.EmployeeId = employee.Id;
            appointment.StartDate = request.StartDate;
            appointment.EndDate = request.EndDate;
            appointment.Price = request.Price;
            appointment.AppointmentHairServices = hairServices
                .Select(hairService => new AppointmentHairService()
                {
                    // Save only the HairServiceId, because AppointmentId still doesn't exist, it will exist only after the row is inserted in the Appointments table, and after that,
                    // EF Core will know how to make the link between the Id from the Appointment table and the AppointmentId from the AppointmentsHairService table.
                    HairServiceId = hairService.Id
                })
                .ToList();

            await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);
            await _unitOfWork.SaveAsync();

            return appointment;
        }
    }
}
