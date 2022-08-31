using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var hairService = await _unitOfWork.HairServiceRepository.GetHairServiceByIdAsync(request.Id);

            if (hairService == null) return null;

            await _unitOfWork.HairServiceRepository.DeleteHairServiceAsync(request.Id);
            await _unitOfWork.SaveAsync();

            return hairService;
        }
    }
}
