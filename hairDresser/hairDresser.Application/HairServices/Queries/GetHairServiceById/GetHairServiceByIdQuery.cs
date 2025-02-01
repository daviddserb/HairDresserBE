using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Queries.GetHairServiceById
{
    public class GetHairServiceByIdQuery : IRequest<HairService>
    {
        public int HairServiceId { get; set; }
    }
}
