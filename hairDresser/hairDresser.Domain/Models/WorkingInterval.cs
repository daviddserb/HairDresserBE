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
        // WorkingDay WorkingDay (= navigational property) iti ofera posibilitatea de a face legatura intre WorkingInterval si Day, adica iti da posibilitatea de a face JOIN intre cele 2 tabele.
        public WorkingDay WorkingDay { get; set; }
        public int WorkingDayId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
