using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByCustomerId
{
    public class GetInWorkAppointmentsByCustomerIdQuery : IRequest<IQueryable<Appointment>>
    {
        public string CustomerId { get; set; }
    }
}
