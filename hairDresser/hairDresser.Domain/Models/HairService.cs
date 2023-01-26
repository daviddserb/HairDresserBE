using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class HairService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }

        public ICollection<AppointmentHairService> AppointmentHairServices { get; set; }
        public ICollection<EmployeeHairService> EmployeeHairServices { get; set; }
    }
}
