using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Commands.DeleteHairService
{
    public class DeleteHairServiceCommand : IRequest<HairService>
    {
        public int HairServiceId { get; set; }
    }
}
