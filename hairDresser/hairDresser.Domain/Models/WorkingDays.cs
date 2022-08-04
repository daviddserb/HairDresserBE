using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class WorkingDays
    {
        public string Name { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }

        public override string ToString() => $"{Name} {startTime} {endTime}";

        public static List<WorkingDays> GenerateWorkingDays()
        {
            return new List<WorkingDays>
            {
                new WorkingDays { Name = "Monday", startTime = new TimeSpan(08, 00, 00), endTime = new TimeSpan(16, 00, 00)},
                new WorkingDays { Name = "Tuesday", startTime = new TimeSpan(08, 30, 00), endTime = new TimeSpan(16, 30, 00)},
                new WorkingDays { Name = "Wednesday", startTime = new TimeSpan(09, 00, 00), endTime = new TimeSpan(17, 00, 00)},
                new WorkingDays { Name = "Thursday", startTime = new TimeSpan(09, 30, 00), endTime = new TimeSpan(17, 30, 00)},
                new WorkingDays { Name = "Friday", startTime = new TimeSpan(10, 00, 00), endTime = new TimeSpan(18, 00, 00)},
            };
        }
    }
}
