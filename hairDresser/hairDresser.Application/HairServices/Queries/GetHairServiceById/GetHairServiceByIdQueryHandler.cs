using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Queries.GetHairServiceById
{
    public class GetHairServiceByIdQueryHandler : IRequestHandler<GetHairServiceByIdQuery, HairService>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHairServiceByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HairService> Handle(GetHairServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var hairService = await _unitOfWork.HairServiceRepository.GetHairServiceByIdAsync(request.HairServiceId);
            if (hairService == null) throw new NotFoundException($"There is no hair service registered with the id '{request.HairServiceId}'!");
            return hairService;
        }
    }
}
