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
        void CreateHairService(HairService hairService);
        IEnumerable<HairService> GetAllHairServices();
        void UpdateHairService(string hairServiceName);
        void DeleteHairService(string hairServiceName);
    }
}
