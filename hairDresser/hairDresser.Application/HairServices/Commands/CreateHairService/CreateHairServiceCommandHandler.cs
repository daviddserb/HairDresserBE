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
    public class CreateHairServiceCommandHandler : IRequestHandler<CreateHairServiceCommand>
    {
        private readonly IHairServiceRepository _hairServiceRepository;

        public CreateHairServiceCommandHandler(IHairServiceRepository hairServiceRepository)
        {
            _hairServiceRepository = hairServiceRepository;
        }

        public async Task<Unit> Handle(CreateHairServiceCommand request, CancellationToken cancellationToken)
        {
            var hairService = new HairService
            {
                Name = request.Name,
                Duration = TimeSpan.FromMinutes(request.DurationInMinutes),
                Price = request.Price
            };
            await _hairServiceRepository.CreateHairServiceAsync(hairService);
            return Unit.Value;
        }
    }
}
