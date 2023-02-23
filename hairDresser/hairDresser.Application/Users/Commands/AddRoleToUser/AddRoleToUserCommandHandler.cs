using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public AddRoleToUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            //var user = await _userManager.FindByNameAsync(request.Username);
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(request.Username);
            if (user == null) throw new NotFoundException("Account does not exist!");

            //var role = await _roleManager.FindByNameAsync(request.Role);
            var role = await _unitOfWork.UserRepository.GetRoleByNameAsync(request.Role);
            // If role doesn't exist then create it.
            if (role == null)
            {
                //await _roleManager.CreateAsync(new IdentityRole
                //{
                //    Name = request.Role
                //});
                await _unitOfWork.UserRepository.CreateRoleAsync(request.Role);
            }

            //var addRoleToUser = await _userManager.AddToRoleAsync(user, request.Role);
            //if (!addRoleToUser.Succeeded)
            //{
            //    // This can be possible, for example, when you try to add the same role twice to the same user.
            //    throw new ClientException($"Failed to add the '{role.Name}' role to the '{user.UserName}' user!");
            //}
            await _unitOfWork.UserRepository.AddRoleToUserAsync(user, request.Role);

            return Unit.Value;
        }
    }
}
