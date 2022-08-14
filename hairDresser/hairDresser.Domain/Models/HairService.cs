using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class HairService
    {
        public string ServiceName { get; set; }
        public TimeSpan Duration { get; set; }
        public float Price { get; set; }
    }
}
