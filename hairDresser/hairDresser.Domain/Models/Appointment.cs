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

        public string CustomerId { get; set; } // scalar property
        public User Customer { get; set; } //navigation property

        public string EmployeeId { get; set; }
        public User Employee { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public DateTime? isDeleted { get; set; }

        public int? ReviewId { get; set; }
        public Review Review { get; set; }

        public ICollection<AppointmentHairService> AppointmentHairServices { get; set; }
    }
}
