using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Queries.GetEmployeeFreeIntervalsByDate
{
    public class GetEmployeeFreeIntervalsByDateQuery : IRequest<List<EmployeeFreeInterval>>
    {
        public string EmployeeId { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        // Date = number that represents the day of the mounth (1, 2, 3, ..., 31).
        public int Date { get; set; }

        public int DurationInMinutes { get; set; }

        public string CustomerId { get; set; }
    }
}
