using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserWithToken>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserWithToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(request.Username);

            if (user != null && await _unitOfWork.UserRepository.CheckUserPasswordAsync(user, request.Password))
            {
                var userRoles = await _unitOfWork.UserRepository.GetUserRolesAsync(user);

                // If the user exists, after the login, we need to return a token, and to do that we need to add some Claims, and will be visible in the token.
                var authClaims = new List<Claim>
                {
                    new Claim("username", request.Username),
                    new Claim("password", request.Password)
                };

                // Add the roles, from the DB, from the specific user, to the claims.
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Generate the key (which is the same as the key from the Program.cs).
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdee-312423d-dsa213321"));

                var token = new JwtSecurityToken
                    (
                    issuer: "https://localhost:7192", // back-end
                    audience: "https://localhost:4200", // front-end
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(10),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new UserWithToken
                {
                    Id = user.Id,
                    Username = request.Username,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            throw new NotFoundException("Account doesn't exist!");
        }
    }
}
