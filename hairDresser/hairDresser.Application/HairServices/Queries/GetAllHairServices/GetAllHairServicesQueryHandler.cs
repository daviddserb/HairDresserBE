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
            Console.Write("Handler -> All the hair services that we can offer:\n");
            var allServices = await _hairServiceRepository.GetAllHairServicesAsync();
            foreach (var service in allServices)
            {
                Console.WriteLine($"name= '{service.Name}', duration= '{service.Duration}', price= '{service.Price}'");
            }
            return await Task.FromResult(allServices);
        }
    }
}
