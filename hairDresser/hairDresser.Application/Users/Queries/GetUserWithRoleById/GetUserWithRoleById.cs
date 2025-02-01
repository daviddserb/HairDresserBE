using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetUserWithRoleById
{
    public class GetUserWithRoleById : IRequest<UserWithRole>
    {
        public string UserId { get; set; }
    }
}
