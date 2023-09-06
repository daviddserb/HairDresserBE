using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Commands.UpdateHairService
{
    public class UpdateHairServiceCommandHandler : IRequestHandler<UpdateHairServiceCommand, HairService>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHairServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HairService> Handle(UpdateHairServiceCommand request, CancellationToken cancellationToken)
        {
            var hairService = await _unitOfWork.HairServiceRepository.GetHairServiceByIdAsync(request.Id);
            if (hairService == null) throw new NotFoundException($"There is no hair service registered with the id '{request.Id}'!");

            hairService.Name = request.Name;
            hairService.Duration = TimeSpan.FromMinutes(request.DurationInMinutes);
            hairService.Price = request.Price;

            await _unitOfWork.HairServiceRepository.UpdateHairServiceAsync(hairService);
            await _unitOfWork.SaveAsync();

            return hairService;
        }
    }
}