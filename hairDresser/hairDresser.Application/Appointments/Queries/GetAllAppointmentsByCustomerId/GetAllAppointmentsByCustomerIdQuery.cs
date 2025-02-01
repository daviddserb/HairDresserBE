using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId
{
    public class GetAllAppointmentsByCustomerIdQuery : IRequest<IQueryable<Appointment>>
    {
        public string CustomerId { get; set; }
    }
}
