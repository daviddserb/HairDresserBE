using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Queries.GetMissingHairServicesByEmployeeId
{
    public class GetMissingHairServicesByEmployeeIdQuery : IRequest<IQueryable<HairService>>
    {
        public string EmployeeId { get; set; }
    }
}
