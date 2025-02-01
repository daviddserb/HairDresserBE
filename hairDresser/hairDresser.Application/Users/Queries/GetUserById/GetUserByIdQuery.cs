using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public string UserId { get; set; }
    }
}
