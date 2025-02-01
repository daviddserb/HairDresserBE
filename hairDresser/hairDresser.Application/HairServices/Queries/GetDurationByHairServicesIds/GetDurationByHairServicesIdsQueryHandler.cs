using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using MediatR;

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
            var hairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesByIdsAsync(request.HairServicesIds);
            if (hairServices == null) throw new NotFoundException("Can't calculate the duration because not all hair services ids are registered!");

            return await _unitOfWork.HairServiceRepository.GetDurationByHairServicesIdsAsync(request.HairServicesIds);
        }
    }
}
