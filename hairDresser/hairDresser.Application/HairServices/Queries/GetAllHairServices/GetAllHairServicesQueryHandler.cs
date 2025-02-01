using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

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
            var allHairServices = await _unitOfWork.HairServiceRepository.GetAllHairServicesAsync();
            if (!allHairServices.Any()) throw new NotFoundException("There are no hair services registered!");
            return allHairServices;
        }
    }
}
