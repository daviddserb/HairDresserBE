using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointments
{
    public class GetAllAppointmentsQuery : IRequest<IQueryable<Appointment>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
