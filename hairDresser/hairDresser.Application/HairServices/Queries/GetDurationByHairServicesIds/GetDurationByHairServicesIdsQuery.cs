using MediatR;

namespace hairDresser.Application.HairServices.Queries.GetDurationByHairServicesIds
{
    public class GetDurationByHairServicesIdsQuery : IRequest<TimeSpan>
    {
        public List<int> HairServicesIds { get; set; }
    }
}
