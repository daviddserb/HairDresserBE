using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _unitOfWork.UserRepository.GetAllUsersWithEmployeeRoleAsync(request.EmployeeIds);
        }
    }
}
