using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //!!! Ar fi ok (nu neaparat) ca denumirea de o Colectie sa aiba s la plural, pt. ca pe ex. nostru, un Appointment are mai multe HairService-uri si atunci trebuia numita: public ICollection<AppointmentHairService> AppointmentHairServices { get; set; }
        public ICollection<AppointmentHairService> AppointmentHairService { get; set; }

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
