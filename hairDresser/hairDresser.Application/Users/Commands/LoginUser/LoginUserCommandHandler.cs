using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            if (user == null || !await _unitOfWork.UserRepository.CheckUserPasswordAsync(user, request.Password))
                throw new NotFoundException("Account doesn't exist!");

            var authClaims = new List<Claim>
            {
                new Claim("username", request.Username),
                new Claim("password", request.Password)
            };

            var userRoles = await _unitOfWork.UserRepository.GetUserRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            //Generate the key (same from Program.cs).
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdee-312423d-dsa213321"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7192",
                audience: "https://localhost:4200",
                claims: authClaims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new UserWithToken
            {
                Id = user.Id,
                Username = request.Username,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}