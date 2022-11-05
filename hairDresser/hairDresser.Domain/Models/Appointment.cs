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

        // Customer Customer it's a navigational property. Gives the possibility to comunicate between 2 tables and to make JOIN operations.
        public User User { get; set; }
        //public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        //public Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
        public DateTime? isDeleted { get; set; }

        public ICollection<AppointmentHairService> AppointmentHairServices { get; set; }
    }
}
