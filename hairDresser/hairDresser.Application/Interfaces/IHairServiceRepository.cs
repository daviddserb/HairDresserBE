using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IHairServiceRepository
    {
        Task CreateHairServiceAsync(HairService hairService);

        Task<IQueryable<HairService>> GetAllHairServicesAsync();
        Task<HairService> GetHairServiceByIdAsync(int hairServiceId);

        Task<IQueryable<EmployeeHairService>> GetAcquiredHairServicesByEmployeeIdAsync(string employeeId);
        Task<IQueryable<HairService>> GetMissingHairServicesByEmployeeIdAsync(string employeeId);

        Task<IQueryable<HairService>> GetAllHairServicesByIdsAsync(List<int> hairServicesId);
        Task<TimeSpan> GetDurationByHairServicesIdsAsync(List<int> hairServicesIds);
        Task<decimal> GetPriceByHairServicesIdsAsync(List<int> hairServicesIds);

        Task<HairService> UpdateHairServiceAsync(HairService hairService);

        Task DeleteHairServiceAsync(int hairServiceId);
    }
}
