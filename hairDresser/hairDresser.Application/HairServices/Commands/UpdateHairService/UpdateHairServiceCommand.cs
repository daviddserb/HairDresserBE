using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Commands.UpdateHairService
{
    public class UpdateHairServiceCommand : IRequest<HairService>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Price { get; set; }
    }
}
