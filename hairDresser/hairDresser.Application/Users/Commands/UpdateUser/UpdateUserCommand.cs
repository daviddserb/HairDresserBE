using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<User>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
    }
}