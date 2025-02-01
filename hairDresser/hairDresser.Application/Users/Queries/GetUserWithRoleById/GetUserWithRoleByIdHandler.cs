using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetUserWithRoleById
{
    public class GetUserWithRoleByIdHandler : IRequestHandler<GetUserWithRoleById, UserWithRole>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserWithRoleByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserWithRole> Handle(GetUserWithRoleById request, CancellationToken cancellationToken)
        {
            var userWithRole = await _unitOfWork.UserRepository.GetUserWithRoleByIdAsync(request.UserId);
            if (userWithRole == null) throw new NotFoundException($"The user with the '{request.UserId}' id does not exist!");
            return userWithRole;
        }
    }
}
