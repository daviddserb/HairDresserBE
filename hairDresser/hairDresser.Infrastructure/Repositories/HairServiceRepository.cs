using hairDresser.Application.Interfaces;
using hairDresser.Domain;
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
        private readonly DataContext context;

        public HairServiceRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task CreateHairServiceAsync(HairService hairService)
        {
            await context.HairServices.AddAsync(hairService);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HairService>> GetHairServiceAsync(List<int> hairServicesId)
        {
            return context.HairServices.Where(service => hairServicesId.Contains(service.Id));
        }

        public async Task<IEnumerable<HairService>> ReadHairServicesAsync()
        {
            return context.HairServices;
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
