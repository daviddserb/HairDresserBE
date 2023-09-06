using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Commands.DeleteHairService
{
    public class DeleteHairServiceCommandHandler : IRequestHandler<DeleteHairServiceCommand, HairService>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHairServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HairService> Handle(DeleteHairServiceCommand request, CancellationToken cancellationToken)
        {
            var hairService = await _unitOfWork.HairServiceRepository.GetHairServiceByIdAsync(request.HairServiceId);
            if (hairService == null) throw new NotFoundException($"There is no hair service registered with the id '{request.HairServiceId}'!");

            await _unitOfWork.HairServiceRepository.DeleteHairServiceAsync(request.HairServiceId);
            await _unitOfWork.SaveAsync();

            return hairService;
        }
    }
}