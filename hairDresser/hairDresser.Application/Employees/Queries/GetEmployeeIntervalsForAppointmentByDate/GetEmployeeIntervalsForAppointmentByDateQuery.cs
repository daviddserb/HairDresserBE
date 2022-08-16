using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Queries.GetEmployeeIntervalsForAppointmentByDate
{
    // ??? Eu vreau sa returnez intervalele valide pt. appointments de la alesul employee, atunci acest CQRS trebuie sa fie in folderul de Appointments/Employees sau care?
    public class GetEmployeeIntervalsForAppointmentByDateQuery : IRequest<List<(DateTime startDate, DateTime endDate)>>
    {
        public int EmployeeId { get; set; }
        public int Date { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
