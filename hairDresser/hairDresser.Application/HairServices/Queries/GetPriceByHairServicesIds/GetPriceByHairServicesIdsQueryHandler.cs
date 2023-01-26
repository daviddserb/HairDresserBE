using hairDresser.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetPriceByHairServicesIds
{
    public class GetPriceByHairServicesIdsQueryHandler : IRequestHandler<GetPriceByHairServicesIdsQuery, decimal>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPriceByHairServicesIdsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> Handle(GetPriceByHairServicesIdsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.HairServiceRepository.GetPriceByHairServicesIds(request.HairServicesIds);
        }
    }
}
