using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Commands.ReviewAppointment
{
    public class ReviewAppointmentCommand : IRequest<Appointment>
    {
        public string CustomerId { get; set; }
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
