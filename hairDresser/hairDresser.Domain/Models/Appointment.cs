using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class Appointment
    {
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public string HairServiceName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Pt. Assignment de la Exception:
        //public void CheckIfAppointmentValid(Appointment app1)
        //{
        //    Console.WriteLine("s-a intrat in CheckIfAppointmentValid()");
        //    int res = DateTime.Compare(app1.StartDate, app1.EndDate);
        //    Console.WriteLine("res=" + res);
        //    if (res > 0)
        //    {
        //        throw new InvalidAppointmentException();
        //    }
        //}
    }
}
