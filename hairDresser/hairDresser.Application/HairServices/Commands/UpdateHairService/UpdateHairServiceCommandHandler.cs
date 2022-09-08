using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (hairService == null) return null;

            hairService.Name = request.Name;
            hairService.Duration = TimeSpan.FromMinutes((double)request.DurationInMinutes);
            hairService.Price = (float)request.Price;

            await _unitOfWork.HairServiceRepository.UpdateHairServiceAsync(hairService);
            await _unitOfWork.SaveAsync();

            return hairService;
        }
    }
}