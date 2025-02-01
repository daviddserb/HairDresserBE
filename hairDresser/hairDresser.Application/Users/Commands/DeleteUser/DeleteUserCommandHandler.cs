using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);
            if (user == null) throw new NotFoundException($"The user with the id '{request.UserId}' does not exist!");

            await _unitOfWork.UserRepository.DeleteUserAsync(request.UserId);
            await _unitOfWork.SaveAsync();

            return user;
        }
    }
}
