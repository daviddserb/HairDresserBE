using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingDays.Commands.CreateWorkingDay
{
    public class CreateWorkingDayCommand : IRequest
    {
        public int EmployeeId { get; set; }
        public int DayId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
