using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Queries.GetAllHairServicesByEmployeeId
{
    public class GetAllHairServicesByEmployeeIdQuery : IRequest<IQueryable<EmployeeHairService>>
    {
        public string EmployeeId { get; set; }
    }
}
