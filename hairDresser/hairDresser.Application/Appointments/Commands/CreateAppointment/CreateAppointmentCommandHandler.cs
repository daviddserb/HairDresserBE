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
            var appointment = new Appointment();

            var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.CustomerId);
            if (customer == null) return null;

            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
            if (employee == null) return null;

            var hairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesByIdsAsync(request.HairServicesIds);
            if (hairServices == null) return null;

            appointment.CustomerId = customer.Id;
            appointment.EmployeeId = employee.Id;
            appointment.AppointmentHairServices = request.HairServicesIds.Select(hairServiceId => new AppointmentHairService()
            {
                // Salvez doar id-ul de la HairServiceId, pt. ca id-ul pt. AppointmentId inca nu exista (nu il stiu), ci va exista dupa ce va fi inserat in tabela Appointments, si apoi EF il va lua de acolo si il salveaza in AppointmentsHairService.
                HairServiceId = hairServiceId
            }).ToList();
            appointment.StartDate = request.StartDate;
            appointment.EndDate = request.EndDate;

            await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);
            await _unitOfWork.SaveAsync();

            return appointment;
        }
    }
}
