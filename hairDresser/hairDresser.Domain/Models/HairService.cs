using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class HairService
    {
        // ? Aceste 3 proprietati nu stiu daca au nevoie de set, pt. ca doar eu le setez, adica Customers nu pot sa le schimbe.
        public string ServiceName { get; set; }
        public string Duration { get; set; }
        public int Price { get; set; }
    }
}
