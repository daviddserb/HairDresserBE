using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<Appointment>
    {
        public int AppointmentId { get; set; }
    }
}
