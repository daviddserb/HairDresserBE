using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.HairServices.Queries
{
    public record GetAllHairServicesQuery : IRequest<IQueryable<HairService>>;
}
