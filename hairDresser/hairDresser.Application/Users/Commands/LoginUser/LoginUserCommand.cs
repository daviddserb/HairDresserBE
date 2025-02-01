using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserWithToken>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
