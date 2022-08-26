using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId
{
    public class GetAllAppointmentsByCustomerIdQueryHandler : IRequestHandler<GetAllAppointmentsByCustomerIdQuery, IQueryable<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAllAppointmentsByCustomerIdQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IQueryable<Appointment>> Handle(GetAllAppointmentsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetAllppointmentsByCustomerIdAsync(request.CustomerId);
        }
    }
}
