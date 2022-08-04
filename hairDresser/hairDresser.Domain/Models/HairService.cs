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

        // Returneaza ce inseamna reprezentarea in string a unui HairService.
        public override string ToString() => $"{ServiceName} {Duration} {Price}";

        public static List<HairService> GenerateHairServices()
        {
            return new List<HairService>
            {
                new HairService { ServiceName = "wash", Duration = new TimeSpan(00, 30, 00), Price = 100},
                new HairService { ServiceName = "cut", Duration = new TimeSpan(01, 00, 00), Price = 100},
                new HairService { ServiceName = "dye", Duration = new TimeSpan(01, 30, 00), Price = 100},
            };
        }
    }
}
