using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointments
{
    public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, IQueryable<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAllAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IQueryable<Appointment>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.ReadAppointmentsAsync();
        }
    }
}
