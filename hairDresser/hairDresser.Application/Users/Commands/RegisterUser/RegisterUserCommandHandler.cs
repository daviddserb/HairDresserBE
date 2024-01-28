using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(request.Username);
            if (user != null) throw new ClientException("Username already exists!");

            user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                Address = request.Address,
                PhoneNumber = request.Phone
            };

            await _unitOfWork.UserRepository.CreateUserAsync(user, request.Password);
            return user;
        }
    }
}