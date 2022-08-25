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
        Task<IEnumerable<HairService>> GetHairServicesByIdsAsync(List<int> hairServicesId);
        Task<IEnumerable<HairService>> ReadHairServicesAsync();
        Task UpdateHairServiceAsync(int hairServiceId);
        Task DeleteHairServiceAsync(int hairServiceId);
    }
}
