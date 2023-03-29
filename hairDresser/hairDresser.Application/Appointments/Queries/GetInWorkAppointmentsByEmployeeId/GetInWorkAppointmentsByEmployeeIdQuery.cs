using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByEmployeeId
{
    public class GetInWorkAppointmentsByEmployeeIdQuery : IRequest<IQueryable<Appointment>>
    {
        public string EmployeeId { get; set; }
    }
}
