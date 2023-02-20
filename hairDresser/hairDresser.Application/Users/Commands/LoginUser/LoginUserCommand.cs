using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserToken>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
