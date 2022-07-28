using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser
{
    // Appointments = the actual schedules with the date time (start-end).
    public class Appointments
    {
        public string ClientName { get; set; }
        public string HairServices { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
