using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetAllUsersWithEmployeeRole
{
    public class GetAllUsersWithEmployeeRoleQueryHandler : IRequestHandler<GetAllUsersWithEmployeeRoleQuery, IQueryable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersWithEmployeeRoleQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<User>> Handle(GetAllUsersWithEmployeeRoleQuery request, CancellationToken cancellationToken)
        {
            var allEmployees = await _unitOfWork.UserRepository.GetAllUsersWithEmployeeRoleAsync();
            if (!allEmployees.Any()) throw new NotFoundException("No employees found!");
            return allEmployees;
        }
    }
}
