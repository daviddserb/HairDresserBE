using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommand : IRequest<Appointment>
    {
        public int AppointmentId { get; set; }
        public int EmployeeId { get; set; }
        public List<int> HairServicesId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
