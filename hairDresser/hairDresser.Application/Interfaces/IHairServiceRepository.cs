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
        Task<IEnumerable<HairService>> GetAllHairServicesAsync();
        Task UpdateHairServiceAsync(string hairServiceName);
        Task DeleteHairServiceAsync(string hairServiceName);
    }
}
