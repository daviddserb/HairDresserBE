using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointmentsByEmployeeId
{
    public class GetAllAppointmentsByEmployeeIdQuery : IRequest<IQueryable<Appointment>>
    {
        public int EmployeeId { get; set; }
    }
}
