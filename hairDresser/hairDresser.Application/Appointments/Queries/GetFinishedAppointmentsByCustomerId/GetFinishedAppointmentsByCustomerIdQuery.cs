using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetFinishedAppointmentsByCustomerId
{
    public class GetFinishedAppointmentsByCustomerIdQuery : IRequest<IQueryable<Appointment>>
    {
        public string CustomerId { get; set; }
    }
}
