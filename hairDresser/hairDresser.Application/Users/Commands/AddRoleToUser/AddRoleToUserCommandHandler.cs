using hairDresser.Application.CustomExceptions;
using hairDresser.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Commands.AddRoleToUser
{
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleToUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) throw new NotFoundException("Account does not exist!");

            var role = await _roleManager.FindByNameAsync(request.Role);
            if (role == null)
            {
                // If role doesn't exist, then create it.
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = request.Role
                });
            }

            var addRoleToUser = await _userManager.AddToRoleAsync(user, request.Role);

            if (!addRoleToUser.Succeeded)
            {
                // This can be possible when you try to add the same role twice to the same user.
                throw new ClientException($"Failed to add the {role.Name} to {user.UserName}!");
            }

            return Unit.Value;
        }
    }
}
