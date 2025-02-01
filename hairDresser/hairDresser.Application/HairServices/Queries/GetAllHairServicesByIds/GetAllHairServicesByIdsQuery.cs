using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Queries.GetAllHairServicesByIds
{
    public class GetAllHairServicesByIdsQuery : IRequest<IQueryable<HairService>>
    {
        public List<int> HairServicesIds { get; set; }
    }
}
