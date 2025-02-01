using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetEmployeesByHairServices
{
    public record GetEmployeesByHairServicesQuery(List<int> HairServicesId) : IRequest<IQueryable<User>>;
}
