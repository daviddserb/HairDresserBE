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

namespace hairDresser.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user != null) throw new ClientException("User account already exists!");

            user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.Phone,
                Address = request.Address,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                // OAuth has some validations on its own columns. Some of the requirements:
                // Password must have at least one: uppercase letter (A, ...), digit (1, ...) and alphanumeric character (symbols: #, @, %, ...).
                // Username can't have space inside it.
                throw new ClientException("Failed to create the account!");
            }

            return user;
        }
    }
}
