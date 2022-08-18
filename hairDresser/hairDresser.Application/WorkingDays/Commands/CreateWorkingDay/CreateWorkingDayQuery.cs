using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingDays.Commands.CreateWorkingDay
{
    public class CreateWorkingDayQuery : IRequest
    {
        public int EmployeeId { get; set; }
        public string NameOfDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
