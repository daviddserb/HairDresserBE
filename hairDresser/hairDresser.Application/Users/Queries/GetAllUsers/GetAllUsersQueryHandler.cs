using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IQueryable<UserWithRole>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetAllUsersQueryHandler(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IQueryable<UserWithRole>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.Select(user => new UserWithRole
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.PhoneNumber,
                // ??? sa vad daca ii vreo diferenta daca iau rolul cu _roleManager
                Role = string.Join(", ", _userManager.GetRolesAsync(user).Result.ToArray())
            });
            return users;
        }
    }
}
