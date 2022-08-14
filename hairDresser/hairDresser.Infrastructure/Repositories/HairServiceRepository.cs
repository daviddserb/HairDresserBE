using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class HairServiceRepository : IHairServiceRepository
    {
        public List<HairService> HairServiceList = new List<HairService>();

        public HairServiceRepository()
        {
            HairServiceList.Add(new HairService { ServiceName = "wash", Duration = new TimeSpan(00, 30, 00), Price = 50 });
            HairServiceList.Add(new HairService { ServiceName = "cut", Duration = new TimeSpan(01, 00, 00), Price = 100 });
            HairServiceList.Add(new HairService { ServiceName = "dye", Duration = new TimeSpan(01, 30, 00), Price = 150 });
        }

        public void CreateHairService(HairService hairService)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HairService> GetAllHairServices()
        {
            return HairServiceList;
        }

        public void UpdateHairService(string hairServiceName)
        {
            throw new NotImplementedException();
        }

        public void DeleteHairService(string hairServiceName)
        {
            throw new NotImplementedException();
        }
    }
}
