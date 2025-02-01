using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<Appointment>
    {
        public int AppointmentId { get; set; }
    }
}
