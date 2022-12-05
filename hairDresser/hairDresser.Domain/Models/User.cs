using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class User : IdentityUser
    {
        public string Address { get; set; }

        public ICollection<Appointment> CustomerAppointments { get; set; }
        public ICollection<Appointment> EmployeeAppointments { get; set; }

        public ICollection<EmployeeHairService> EmployeeHairServices { get; set; }

        public ICollection<WorkingInterval> EmployeeWorkingIntervals { get; set; }
    }
}
