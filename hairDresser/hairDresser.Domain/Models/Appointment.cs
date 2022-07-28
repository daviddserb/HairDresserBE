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
        public Appointment(string CustomerName, string EmployeeName, string HairServiceName, DateTime StartDate, DateTime EndDate)
        {
            // The 'this' points (->) to the field.
            this.CustomerName = CustomerName;
            this.EmployeeName = EmployeeName;
            this.HairServiceName = HairServiceName;
            this.StartDate = StartDate;
            this.EndDate = EndDate;

        }

        public void CheckIfAppointmentValid(Appointment app1)
        {
            Console.WriteLine("s-a intrat in CheckIfAppointmentValid()");
            int res = DateTime.Compare(app1.StartDate, app1.EndDate);
            Console.WriteLine("res=" + res);
            if (res > 0)
            {
                throw new InvalidAppointmentException();
            }
        }

        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public string HairServiceName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
