using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetAllUsersWithEmployeeRole
{
    public class GetAllUsersWithEmployeeRoleQuery : IRequest<IQueryable<User>> { }
}
