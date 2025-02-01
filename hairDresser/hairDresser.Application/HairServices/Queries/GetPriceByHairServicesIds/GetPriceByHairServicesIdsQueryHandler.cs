using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using MediatR;

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
            var hairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesByIdsAsync(request.HairServicesIds);
            if (hairServices == null) throw new NotFoundException("Can't calculate the price because not all hair services ids are registered!");

            return await _unitOfWork.HairServiceRepository.GetPriceByHairServicesIdsAsync(request.HairServicesIds);
        }
    }
}
