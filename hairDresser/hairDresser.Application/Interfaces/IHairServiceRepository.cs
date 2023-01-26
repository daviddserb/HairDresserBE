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
        Task<IQueryable<HairService>> ReadHairServicesAsync();
        Task<HairService> GetHairServiceByIdAsync(int hairServiceId);
        Task<IQueryable<EmployeeHairService>> GetHairServicesByEmployeeId (string employeeId);
        Task<IQueryable<HairService>> GetMissingHairServicesByEmployeeId(string employeeId);
        Task<IQueryable<HairService>> GetAllHairServicesByIdsAsync(List<int> hairServicesId);
        Task<TimeSpan> GetDurationByHairServicesIds(List<int> hairServicesIds);
        Task<decimal> GetPriceByHairServicesIds(List<int> hairServicesIds);
        Task<HairService> UpdateHairServiceAsync(HairService hairService);
        Task DeleteHairServiceAsync(int hairServiceId);
    }
}
