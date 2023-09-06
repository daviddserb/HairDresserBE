using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetAllHairServicesByIds
{
    public class GetAllHairServicesByIdsQueryHandler : IRequestHandler<GetAllHairServicesByIdsQuery, IQueryable<HairService>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllHairServicesByIdsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<HairService>> Handle(GetAllHairServicesByIdsQuery request, CancellationToken cancellationToken)
        {
            var hairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesByIdsAsync(request.HairServicesIds);
            if (hairServices == null) throw new NotFoundException("Not all hair services ids are registered!");
            return hairServices;
        }
    }
}
