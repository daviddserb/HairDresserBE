using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IQueryable<UserWithRole>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<UserWithRole>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAllUsersAsync();
            return users;
        }
    }
}
