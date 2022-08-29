using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByCustomerId
{
    public class GetInWorkAppointmentsByCustomerIdQuery : IRequest<IQueryable<Appointment>>
    {
        public int CustomerId { get; set; }
    }
}
