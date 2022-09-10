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
            appointment.StartDate = request.StartDate;
            appointment.EndDate = request.EndDate;
            appointment.AppointmentHairServices = request.HairServicesIds
                .Select(hairServiceId => new AppointmentHairService()
                {
                    // Save only the HairServiceId, because AppointmentId still doesn't exist, it will exist only after the row is inserted in the Appointments table, and after EF Core will
                    //know how to make the link between the Id from the Appointment table and the AppointmentId from the AppointmentsHairService table.
                    HairServiceId = hairServiceId
                })
                .ToList();

            await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);
            await _unitOfWork.SaveAsync();

            return appointment;
        }
    }
}
