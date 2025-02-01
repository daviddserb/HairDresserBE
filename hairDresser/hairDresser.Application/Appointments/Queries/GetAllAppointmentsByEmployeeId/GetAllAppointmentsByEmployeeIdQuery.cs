using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointmentsByEmployeeId
{
    public class GetAllAppointmentsByEmployeeIdQuery : IRequest<IQueryable<Appointment>>
    {
        public string EmployeeId { get; set; }
    }
}
