using MediatR;

namespace hairDresser.Application.HairServices.Queries.GetPriceByHairServicesIds
{
    public class GetPriceByHairServicesIdsQuery : IRequest<decimal>
    {
        public List<int> HairServicesIds { get; set; }
    }
}
