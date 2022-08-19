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
            _hairServices.Add(new HairService { Id = 1, Name = "wash", Duration = new TimeSpan(00, 30, 00), Price = 50 });
            _hairServices.Add(new HairService { Id = 2, Name = "cut", Duration = new TimeSpan(01, 00, 00), Price = 100 });
            _hairServices.Add(new HairService { Id = 3, Name = "dye", Duration = new TimeSpan(01, 30, 00), Price = 150 });
        }

        // !!! O sa trebuiasca pe viitor sa fac ceva de genul GetDuration unde primesc o lista de Id de HairServices si calculez timpul. La fel si pt. Price.

        public async Task CreateHairServiceAsync(HairService hairService)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<HairService>> GetHairServiceAsync(List<int> hairServicesId)
        {
            var hairServices = _hairServices.Where(obj => hairServicesId.Contains(obj.Id));
            return hairServices;
        }

        public async Task<IEnumerable<HairService>> ReadHairServicesAsync()
        {
            return _hairServices;
        }

        public async Task UpdateHairServiceAsync(int hairServiceId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteHairServiceAsync(int hairServiceId)
        {
            throw new NotImplementedException();
        }
    }
}
