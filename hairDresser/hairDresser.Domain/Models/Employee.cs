using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    // The Employee class doesn't have a relationship with the user, it's just a basic information that will help when set an Appointment.
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
    }
}
