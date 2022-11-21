using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.CreateAppointment
{
    // To use the MediatR library, we need to implemenet IRequest interface, which can receive a parameter or not which represents the returned data type.
    public class CreateAppointmentCommand : IRequest<Appointment>
    {
        // These are all the properties without  I can't make an appointment, that means I need user input in all of them and I will be able to use them in the Handler.
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public List<int> HairServicesIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
    }
}
