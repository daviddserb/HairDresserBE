using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<User>
    {
        public string UserId { get; set; }
    }
}
