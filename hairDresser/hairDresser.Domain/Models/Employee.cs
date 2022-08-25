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
        public ICollection<WorkingDay> WorkingDays { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<EmployeeHairService> EmployeeHairService { get; set; }
    }
}
