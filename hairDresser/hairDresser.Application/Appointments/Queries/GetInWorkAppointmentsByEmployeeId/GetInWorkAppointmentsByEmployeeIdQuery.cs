using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByEmployeeId
{
    public class GetInWorkAppointmentsByEmployeeIdQuery : IRequest<IQueryable<Appointment>>
    {
        public string EmployeeId { get; set; }
    }
}
