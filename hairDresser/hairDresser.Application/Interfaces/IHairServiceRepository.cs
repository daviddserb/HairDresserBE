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
        Task<IQueryable<HairService>> GetHairServicesByIdsAsync(List<int> hairServicesId);
        Task<HairService> UpdateHairServiceAsync(HairService hairService);
        Task DeleteHairServiceAsync(int hairServiceId);
    }
}
