using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class WorkingDay
    {
        public int Id { get; set; }
        // Day Day iti ofera posibilitatea de a face legatura intre WorkingDay si Day, adica iti da posibilitatea de a face JOIN intre cele 2 tabele.
        public Day Day { get; set; }
        public int DayId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
