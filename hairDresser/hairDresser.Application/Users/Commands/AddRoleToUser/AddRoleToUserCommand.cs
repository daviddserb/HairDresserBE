using MediatR;

namespace hairDresser.Application.Users.Commands.AddRoleToUser
{
    public class AddRoleToUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
