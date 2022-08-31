using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Commands.CreateHairService
{
    public class CreateHairServiceCommandHandler : IRequestHandler<CreateHairServiceCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateHairServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateHairServiceCommand request, CancellationToken cancellationToken)
        {
            var hairService = new HairService
            {
                Name = request.Name,
                Duration = TimeSpan.FromMinutes(request.DurationInMinutes),
                Price = request.Price
            };
            await _unitOfWork.HairServiceRepository.CreateHairServiceAsync(hairService);
            await _unitOfWork.SaveAsync();
            return hairService.Id;
        }
    }
}
