using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Queries.GetEmployeeIntervalsForAppointmentByDate
{
    public class GetEmployeeIntervalsByDateQuery : IRequest<List<(DateTime startDate, DateTime endDate)>>
    {
        public int EmployeeId { get; set; }
        public int Date { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
