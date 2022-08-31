using hairDresser.Application.Interfaces;
using hairDresser.Domain;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
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
        }

        public async Task<IQueryable<HairService>> ReadHairServicesAsync()
        {
            return context.HairServices;
        }

        public async Task<HairService> GetHairServiceByIdAsync(int hairServiceId)
        {
            return await context.HairServices.FirstOrDefaultAsync(HairService => HairService.Id == hairServiceId);
        }

        public async Task<IQueryable<HairService>> GetAllHairServicesByIdsAsync(List<int> hairServicesIds)
        {
            return context.HairServices.Where(HairService => hairServicesIds.Contains(HairService.Id));
        }

        public async Task<HairService> UpdateHairServiceAsync(HairService hairService)
        {
            context.HairServices.Update(hairService);
            return hairService;
        }
        public async Task DeleteHairServiceAsync(int hairServiceId)
        {
            var hairService = await context.HairServices.FirstOrDefaultAsync(hairService => hairService.Id == hairServiceId);
            context.HairServices.Remove(hairService);
        }
    }
}
