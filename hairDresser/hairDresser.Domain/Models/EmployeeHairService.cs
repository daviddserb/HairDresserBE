using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class EmployeeHairService
    {
        public int Id { get; set; }

        //public Employee Employee { get; set; }
        public User User { get; set; }
        public Guid EmployeeId { get; set; }

        public HairService HairService { get; set; }
        public int HairServiceId { get; set; }
    }
}
