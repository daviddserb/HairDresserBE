using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EmployeeHairService> EmployeeHairServices { get; set; }

        public ICollection<WorkingInterval> EmployeeWorkingIntervals { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
