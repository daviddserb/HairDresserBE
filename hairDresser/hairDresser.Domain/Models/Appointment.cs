using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    // Appointments = the actual schedules with the date time (start-end).
    public class Appointment
    {
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public string HairServiceName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Appointment(string CustomerName, string EmployeeName, string HairServiceName, DateTime StartDate, DateTime EndDate)
        {
            // The 'this' points (->) to the field.
            this.CustomerName = CustomerName;
            this.EmployeeName = EmployeeName;
            this.HairServiceName = HairServiceName;
            this.StartDate = StartDate;
            this.EndDate = EndDate;

        }
    }
}
