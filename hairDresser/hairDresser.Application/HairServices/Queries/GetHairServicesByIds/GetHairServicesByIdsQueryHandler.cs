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
        private readonly IUnitOfWork _unitOfWork;

        public GetHairServicesByIdsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<HairService>> Handle(GetHairServicesByIdsQuery request, CancellationToken cancellationToken)
        {
            var selectedHairServices = await _unitOfWork.HairServiceRepository.GetHairServicesByIdsAsync(request.HairServicesIds);
            return selectedHairServices;
        }
    }
}
