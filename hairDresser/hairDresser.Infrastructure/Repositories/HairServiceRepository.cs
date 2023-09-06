using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            return context.HairServices;
        }

        public async Task<HairService> GetHairServiceByIdAsync(int hairServiceId)
        {
            return await context.HairServices
                .FirstOrDefaultAsync(HairService => HairService.Id == hairServiceId);
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

        /// <summary>
        /// After modifying the method's body (the query), ensure the Integration Tests are working correctly. It's essential because translating the query into SQLite can potentially lead to issues.
        /// </summary>
        /// <param name="hairServicesIds"></param>
        /// <returns></returns>
        public async Task<TimeSpan> GetDurationByHairServicesIdsAsync(List<int> hairServicesIds)
        {
            var hairServices = await context.HairServices
                .Where(hairService => hairServicesIds.Contains(hairService.Id))
                .ToListAsync();

            int hairServicesDuration = hairServices
                .Sum(hairService => hairService.Duration.Hours * 60 + hairService.Duration.Minutes);

            return TimeSpan.FromMinutes(hairServicesDuration);
        }

        /// <summary>
        /// After modifying the method's body (the query), ensure the Integration Tests are working correctly. It's essential because translating the query into SQLite can potentially lead to issues.
        /// </summary>
        /// <param name="hairServicesIds"></param>
        /// <returns></returns>
        public async Task<decimal> GetPriceByHairServicesIdsAsync(List<int> hairServicesIds)
        {
            var hairServices = await context.HairServices
                .Where(hairService => hairServicesIds.Contains(hairService.Id))
                .ToListAsync();

            decimal hairServicesPrice = hairServices
                .Sum(hairService => hairService.Price);

            return hairServicesPrice;
        }

        public async Task<HairService> UpdateHairServiceAsync(HairService hairService)
        {
            context.HairServices.Update(hairService);
            return hairService;
        }
        public async Task DeleteHairServiceAsync(int hairServiceId)
        {
            var hairService = await context.HairServices
                .FirstOrDefaultAsync(hairService => hairService.Id == hairServiceId);
            context.HairServices.Remove(hairService);
        }
    }
}
