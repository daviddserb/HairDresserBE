using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommand : IRequest<Appointment>
    {
        public string CustomerId { get; set; }
        public int AppointmentId { get; set; }
    }
}
