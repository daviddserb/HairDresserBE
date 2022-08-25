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
        private readonly IHairServiceRepository _hairServiceRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public CreateAppointmentCommandHandler(IHairServiceRepository hairServiceRepository, IAppointmentRepository appointmentRepository)
        {
            _hairServiceRepository = hairServiceRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Unit> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment
            {
                CustomerId = request.CustomerId,
                EmployeeId = request.EmployeeId,
                StartDate = DateTime.Parse(request.StartDate),
                EndDate = DateTime.Parse(request.EndDate),
                AppointmentHairService = request.HairServicesId.Select(hsi => new AppointmentHairService()
                {
                    //Salvez doar id-ul de la hairservices, pt. ca cel de la appointment inca nu exista, ci va exista dupa ce s-a inserat in tabela Appointments si el il va lua de acolo si il salveaza in AppointmentsHairService.
                    HairServiceId = hsi
                }).ToList()
            };
            await _appointmentRepository.CreateAppointmentAsync(appointment);
            return Unit.Value;
        }
    }
}
