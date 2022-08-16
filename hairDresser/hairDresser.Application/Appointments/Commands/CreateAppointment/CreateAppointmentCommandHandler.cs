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
    // Trebuie sa implementam interfata IRequestHandler, care primeste 2 parametrii: request-ul (mesajul) si raspunsul mesajului (ce returneaza el)
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IEmployeeRepository employeeRepository)
        {
            _appointmentRepository = appointmentRepository;
            _employeeRepository = employeeRepository;
        }

        public Task<Appointment> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var employeeName = _employeeRepository.GetEmployeeById(request.EmployeeId).Name;

            var hairServices = "";
            for (int i = 0; i < request.HairServices.Count; ++i)
            {
                hairServices += request.HairServices[i];
                if (i != request.HairServices.Count - 1)
                {
                    hairServices += ", ";
                }
            }

            var appointment = new Appointment { 
                CustomerName = request.CustomerName,
                EmployeeName = employeeName,
                HairServices = hairServices,
                StartDate = DateTime.Parse(request.StartDate),
                EndDate = DateTime.Parse(request.EndDate)
            };

            Console.Write("Handler -> The new appointment:\n");
            Console.WriteLine(appointment.CustomerName + " - " + appointment.EmployeeName + " - " + appointment.HairServices + " - " + appointment.StartDate + " - " + appointment.EndDate);

            _appointmentRepository.CreateAppointment(appointment);
            return Task.FromResult(appointment);
        }
    }
}
