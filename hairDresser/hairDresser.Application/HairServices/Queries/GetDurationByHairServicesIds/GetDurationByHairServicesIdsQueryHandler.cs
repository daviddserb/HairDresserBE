using hairDresser.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetDurationByHairServicesIds
{
    public class GetDurationByHairServicesIdsQueryHandler : IRequestHandler<GetDurationByHairServicesIdsQuery, TimeSpan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDurationByHairServicesIdsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TimeSpan> Handle(GetDurationByHairServicesIdsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.HairServiceRepository.GetDurationByHairServicesIds(request.HairServicesIds);
        }
    }
}
