using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Comments { get; set; }

        public Appointment Appointment { get; set; } // navigation property for the Appointment
    }
}
