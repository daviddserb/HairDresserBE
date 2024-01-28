using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.Id);
            if (user == null) throw new NotFoundException($"The user with the id '{request.Id}' is not registered!");

            // Check if the new username is empty
            if (string.IsNullOrEmpty(user.UserName)) throw new ClientException("Username can't be empty!");

            // Check if the new username contains whitespace, which is an IdentityUser constraint when register user
            if (user.UserName.Contains(" ")) throw new ClientException("Username can't contain whitespaces!");

            // Check if the new username is taken by an existing user which is already in the database
            var userNewUsername = await _unitOfWork.UserRepository.GetUserByUserNameAsync(request.Username);
            if (userNewUsername != null) throw new ClientException("Username already exists!");

            user.UserName = request.Username;
            user.Address = request.Address;

            await _unitOfWork.UserRepository.UpdateUserAsync(user);
            await _unitOfWork.SaveAsync();

            return user;
        }
    }
}