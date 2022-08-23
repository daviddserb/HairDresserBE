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
        // Key = column is PK.
        [Key]
        public int Id { get; set; }

        // Required = column can't be NULL.
        [Required]
        public string CustomerName { get; set; }

        [MaxLength(20)]
        public string EmployeeName { get; set; }

        public string HairServices { get; set; }

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
