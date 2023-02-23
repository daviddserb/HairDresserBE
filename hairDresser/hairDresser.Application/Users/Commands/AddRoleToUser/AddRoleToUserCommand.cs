using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Commands.AddRoleToUser
{
    public class AddRoleToUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
