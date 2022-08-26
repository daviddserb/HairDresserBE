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
        }

        public async Task<IQueryable<HairService>> ReadHairServicesAsync()
        {
            return context.HairServices;
        }

        public async Task<IQueryable<HairService>> GetHairServicesByIdsAsync(List<int> hairServicesIds)
        {
            //Console.WriteLine("HairServiceRepository -> GetHairServiceByIdsAsync");
            var hairServices = context.HairServices.Where(service => hairServicesIds.Contains(service.Id));
            // Daca vreau sa returnez doar id-urile: context.HairServices.Where(service => hairServicesIds.Contains(service.Id)).Select(service => service.Id)
            //foreach (var services in hairServices)
            //{
            //    Console.WriteLine($"{services.Id} - '{services.Name}', '{services.Duration}', '{services.Price}'");
            //}
            return hairServices;
        }

        public async Task<HairService> UpdateHairServiceAsync(HairService hairService)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteHairServiceAsync(int hairServiceId)
        {
            throw new NotImplementedException();
        }
    }
}
