using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class EmployeeHairService
    {
        public int Id { get; set; }

        // BEFORE:
        //public Employee Employee { get; set; }
        //public int EmployeeId { get; set; }

        // AFTER:
        public string EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }

        public int HairServiceId { get; set; }
        public HairService HairService { get; set; }
    }
}
