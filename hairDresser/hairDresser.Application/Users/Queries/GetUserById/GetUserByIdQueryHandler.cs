using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);
            if (user == null) throw new NotFoundException($"The user with the '{request.UserId}' id does not exist!");
            return user;
        }
    }
}
