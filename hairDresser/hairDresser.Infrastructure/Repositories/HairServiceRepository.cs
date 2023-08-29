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

        public async Task<IQueryable<HairService>> GetAllHairServicesAsync()
        {
            var allHairServices = context.HairServices;
            return allHairServices;
        }

        public async Task<HairService> GetHairServiceByIdAsync(int hairServiceId)
        {
            var hairService = await context.HairServices.FirstOrDefaultAsync(HairService => HairService.Id == hairServiceId);
            if (hairService == null) return null;
            return hairService;
        }

        public async Task<IQueryable<EmployeeHairService>> GetAcquiredHairServicesByEmployeeIdAsync(string employeeId) {
            var employeeHairServices = context.EmployeesHairServices
                .Where(employeeHairService => employeeHairService.EmployeeId == employeeId)
                .Include(employeeHairService => employeeHairService.HairService);

            return employeeHairServices;
        }

        public async Task<IQueryable<HairService>> GetMissingHairServicesByEmployeeIdAsync(string employeeId)
        {
            var employeeMissingHairServices2 = context.HairServices
                .Where(hs => !context.EmployeesHairServices.Any(ehs => ehs.EmployeeId == employeeId && ehs.HairServiceId == hs.Id));

            return employeeMissingHairServices2;
        }

        public async Task<IQueryable<HairService>> GetAllHairServicesByIdsAsync(List<int> hairServicesIds)
        {
            var hairServices = context.HairServices
                .Where(hairService => hairServicesIds.Contains(hairService.Id));

            if (hairServices.Count() == hairServicesIds.Count()) return hairServices;
            else return null;
        }

        public async Task<TimeSpan> GetDurationByHairServicesIdsAsync(List<int> hairServicesIds)
        {
            // BEFORE:
            //return context.HairServices
            //    .Where(hairService => hairServicesIds.Contains(hairService.Id))
            //    .Sum(hairServices => hairServices.Duration.Hours * 60 + hairServices.Duration.Minutes);

            // AFTER (because in Integration Test, when calling this method with SQLite, there are some methods that are not compatible):
            var selectedHairServices = await context.HairServices
                .Where(hairService => hairServicesIds.Contains(hairService.Id))
                .ToListAsync();

            int selectedHairServicesTotalDurationInMinutes = selectedHairServices
                .Sum(hairService => hairService.Duration.Hours * 60 + hairService.Duration.Minutes);

            return TimeSpan.FromMinutes(selectedHairServicesTotalDurationInMinutes);
        }

        public async Task<decimal> GetPriceByHairServicesIdsAsync(List<int> hairServicesIds)
        {
            // BEFORE:
            //return context.HairServices
            //    .Where(hairService => hairServicesIds.Contains(hairService.Id))
            //    .Sum(hairServices => hairServices.Price);

            // AFTER (because in Integration Test, when calling this method with SQLite, there are some methods that are not compatible):
            var selectedHairServices = await context.HairServices
                .Where(hairService => hairServicesIds.Contains(hairService.Id))
                .ToListAsync();

            decimal selectedHairServicesTotalPrice = selectedHairServices
                .Sum(hairService => hairService.Price);

            return selectedHairServicesTotalPrice;
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
