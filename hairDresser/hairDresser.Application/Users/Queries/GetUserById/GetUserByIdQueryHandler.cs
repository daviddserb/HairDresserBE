using hairDresser.Application.Customers.Queries.GetCustomerById;
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

namespace hairDresser.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserWithRole>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByIdQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserWithRole> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) throw new NotFoundException($"The user with the '{request.UserId}' id does not exist!");

            var userWithRole = new UserWithRole()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.PhoneNumber,
                // ??? sa rezolv GetAllUsersQueryHandler si apoi sa vad aici daca are rost sa fac ceva
                Role = string.Join(", ", _userManager.GetRolesAsync(user).Result.ToArray())
            };

            return userWithRole;
        }
    }
}
