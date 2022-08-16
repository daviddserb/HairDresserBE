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

        public Task<IEnumerable<HairService>> Handle(GetAllHairServicesQuery request, CancellationToken cancellationToken)
        {
            Console.Write("Handler -> All the hair services that we can offer:\n");
            foreach (var hsr in _hairServiceRepository.GetAllHairServices())
            {
                Console.WriteLine($"name= '{hsr.Name}', duration= '{hsr.Duration}', price= '{hsr.Price}'");
            }
            return Task.FromResult(_hairServiceRepository.GetAllHairServices());
        }
    }
}
