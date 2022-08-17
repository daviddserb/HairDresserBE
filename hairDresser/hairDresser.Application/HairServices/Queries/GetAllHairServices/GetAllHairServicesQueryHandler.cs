using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries
{
    public class GetAllHairServicesQueryHandler : IRequestHandler<GetAllHairServicesQuery, IEnumerable<HairService>>
    {
        private IHairServiceRepository _hairServiceRepository;
        public GetAllHairServicesQueryHandler(IHairServiceRepository hairServiceRepository)
        {
            _hairServiceRepository = hairServiceRepository;
        }

        public async Task<IEnumerable<HairService>> Handle(GetAllHairServicesQuery request, CancellationToken cancellationToken)
        {
            var allServices = await _hairServiceRepository.GetAllHairServicesAsync();
            return allServices;
        }
    }
}
