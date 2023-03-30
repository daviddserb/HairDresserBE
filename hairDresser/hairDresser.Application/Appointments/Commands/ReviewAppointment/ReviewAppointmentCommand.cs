using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.ReviewAppointment
{
    public class ReviewAppointmentCommand : IRequest<Appointment>
    {
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
