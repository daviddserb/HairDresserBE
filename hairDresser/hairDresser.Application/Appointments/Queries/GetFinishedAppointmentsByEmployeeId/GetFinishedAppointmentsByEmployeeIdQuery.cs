using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetFinishedAppointmentsByEmployeeId
{
    public class GetFinishedAppointmentsByEmployeeIdQuery : IRequest<IQueryable<Appointment>>
    {
        public string EmployeeId { get; set; }
    }
}
