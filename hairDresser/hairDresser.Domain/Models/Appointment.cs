using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        // BEFORE:
        //public Customer Customer { get; set; }
        //public int CustomerId { get; set; }
        //public Employee Employee { get; set; }
        //public int EmployeeId { get; set; }
        // AFTER:
        public string CustomerId { get; set; } //scalar property
        [ForeignKey("CustomerId")] // ??? Nu cred ca mai am nevoie pt. ca le-am pus in Fluent API
        public ApplicationUser Customer { get; set; } //navigation property
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")] // ??? Nu cred ca mai am nevoie pt. ca le-am pus in Fluent API
        public ApplicationUser Employee { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
        public DateTime? isDeleted { get; set; }

        public ICollection<AppointmentHairService> AppointmentHairServices { get; set; }
    }
}
