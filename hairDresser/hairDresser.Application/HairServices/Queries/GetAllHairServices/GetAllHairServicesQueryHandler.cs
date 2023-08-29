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
    public class GetAllHairServicesQueryHandler : IRequestHandler<GetAllHairServicesQuery, IQueryable<HairService>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllHairServicesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<HairService>> Handle(GetAllHairServicesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.HairServiceRepository.GetAllHairServicesAsync();
        }
    }
}
