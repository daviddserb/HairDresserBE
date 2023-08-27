using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.CreateAppointment
{
    /// <summary>
    /// To use the MediatR library, we need to implemenet IRequest interface, which can receive a parameter or not, which represents the returned data type of the Handler.
    /// </summary>
    public class CreateAppointmentCommand : IRequest<Appointment>
    {
        /// <summary>
        /// These are all the properties needed to make an appointment, that means I need user input in all of them and I will be able to use them in the Handler class.
        /// </summary>
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public List<int> HairServicesIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
