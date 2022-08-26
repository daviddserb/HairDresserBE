using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetHairServicesByIds
{
    public class GetHairServicesByIdsQueryHandler : IRequestHandler<GetHairServicesByIdsQuery, IQueryable<HairService>>
    {
        private readonly IHairServiceRepository _hairServiceRepository;
        public GetHairServicesByIdsQueryHandler(IHairServiceRepository hairServiceRepository)
        {
            _hairServiceRepository = hairServiceRepository;
        }
        public async Task<IQueryable<HairService>> Handle(GetHairServicesByIdsQuery request, CancellationToken cancellationToken)
        {
            //Console.WriteLine("GetHairServicesByIdsQueryHandler:");
            var selectedHairServices = await _hairServiceRepository.GetHairServicesByIdsAsync(request.HairServicesIds);
            //Doar pt. testing:
            //var name_selectedHairServicesIds = selectedHairServices.Select(service => service.Name);
            //foreach(var selectedHairServicesId in name_selectedHairServicesIds)
            //{
            //    Console.WriteLine(selectedHairServicesId);
            //}
            return selectedHairServices;
        }
    }
}
