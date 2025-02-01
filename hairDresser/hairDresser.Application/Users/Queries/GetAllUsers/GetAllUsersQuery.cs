using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IQueryable<UserWithRole>> { }
}
