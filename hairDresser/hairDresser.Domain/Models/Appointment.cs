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
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        public Customer? Customer { get; set; }

        [Required(ErrorMessage = "Customer id required.")]
        [Range(1, int.MaxValue)]
        public int? CustomerId { get; set; }

        public Employee? Employee { get; set; }

        [Required(ErrorMessage = "Employee id required.")]
        [Range(1, int.MaxValue)]
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Starrrt date id required.")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Enddateeee id required.")]
        public DateTime? EndDate { get; set; }

        public ICollection<AppointmentHairService>? AppointmentHairServices { get; set; }

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
