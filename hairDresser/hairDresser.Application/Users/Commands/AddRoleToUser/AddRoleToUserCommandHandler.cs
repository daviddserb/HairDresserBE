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
        private readonly IUnitOfWork _unitOfWork;

        public AddRoleToUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(request.Username);
            if (user == null) throw new NotFoundException("Account does not exist!");

            var role = await _unitOfWork.UserRepository.GetRoleByNameAsync(request.Role);
            // If role doesn't exist then create it.
            if (role == null)
            {
                await _unitOfWork.UserRepository.CreateRoleAsync(request.Role);
            }

            await _unitOfWork.UserRepository.AddRoleToUserAsync(user, request.Role);

            return Unit.Value;
        }
    }
}
