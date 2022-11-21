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
    // Trebuie sa implementam interfata IRequestHandler, care poate sa primeasca 2 parametrii: request-ul (command/query) care este obligatoriu si raspunsul request-ului (ce returneaza el).
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment();

            var customer = await _unitOfWork.UserRepository.GetUserByIdAsync(request.CustomerId);
            if (customer == null) throw new NotFoundException($"The customer with the id '{request.CustomerId}' does not exist!");

            var employee = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (employee == null) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' does not exist!");

            var hairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesByIdsAsync(request.HairServicesIds);
            if (hairServices == null) throw new NotFoundException($"All or some of the hair services with the ids '{String.Join(", ", request.HairServicesIds)}' do not exist!");

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

            return appointment;
        }
    }
}
