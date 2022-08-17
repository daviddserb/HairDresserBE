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
        private readonly List<HairService> _hairServices = new();

        public HairServiceRepository()
        {
            _hairServices.Add(new HairService { Name = "wash", Duration = new TimeSpan(00, 30, 00), Price = 50 });
            _hairServices.Add(new HairService { Name = "cut", Duration = new TimeSpan(01, 00, 00), Price = 100 });
            _hairServices.Add(new HairService { Name = "dye", Duration = new TimeSpan(01, 30, 00), Price = 150 });
        }

        public async Task CreateHairServiceAsync(HairService hairService)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HairService>> GetAllHairServicesAsync()
        {
            return _hairServices;
        }

        public async Task UpdateHairServiceAsync(string hairServiceName)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteHairServiceAsync(string hairServiceName)
        {
            throw new NotImplementedException();
        }
    }
}
