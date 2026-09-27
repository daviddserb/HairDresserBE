using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetFinishedAppointmentsByEmployeeId
{
    public class GetFinishedAppointmentsByEmployeeIdQuery : IRequest<List<Appointment>>, ICacheableQuery
    {
        public string EmployeeId { get; set; }

        public string CacheKey => $"FinishedAppointments:Employee:{EmployeeId}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(30);
    }
}