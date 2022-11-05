using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class WorkingInterval
    {
        public int Id { get; set; }

        public WorkingDay WorkingDay { get; set; }
        public int WorkingDayId { get; set; }

        public User User { get; set; }
        //public Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
