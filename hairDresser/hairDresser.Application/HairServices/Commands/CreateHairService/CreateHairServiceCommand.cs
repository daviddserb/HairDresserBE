using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Commands.CreateHairService
{
    public class CreateHairServiceCommand : IRequest<HairService>
    {
        public string Name { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Price { get; set; }
    }
}
