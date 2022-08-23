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
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHairServiceRepository _hairServiceRepository;

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IEmployeeRepository employeeRepository, IHairServiceRepository hairServiceRepository)
        {
            _appointmentRepository = appointmentRepository;
            _employeeRepository = employeeRepository;
            _hairServiceRepository = hairServiceRepository;
        }

        public async Task<Unit> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var employeeById = await _employeeRepository.GetEmployeeAsync(request.EmployeeId);
            var employeeName = employeeById.Name;

            var hairServicesPickedByCustomer = await _hairServiceRepository.GetHairServiceAsync(request.HairServicesId);
            var hairServices = "";
            foreach(var service in hairServicesPickedByCustomer)
            {
                hairServices += service.Name;
                if (service != hairServicesPickedByCustomer.Last())
                {
                    hairServices += ", ";
                }
            }

            var appointment = new Appointment
            {
                CustomerName = request.CustomerName,
                EmployeeName = employeeName,
                HairServices = hairServices,
                StartDate = DateTime.Parse(request.StartDate),
                EndDate = DateTime.Parse(request.EndDate)
            };

            await _appointmentRepository.CreateAppointmentAsync(appointment);

            return Unit.Value;
        }
    }
}
