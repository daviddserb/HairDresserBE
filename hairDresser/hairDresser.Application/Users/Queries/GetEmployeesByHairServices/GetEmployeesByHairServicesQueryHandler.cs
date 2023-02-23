using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Queries.GetEmployeesByHairServices
{
    public class GetEmployeesByHairServicesQueryHandler : IRequestHandler<GetEmployeesByHairServicesQuery, IQueryable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeesByHairServicesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<User>> Handle(GetEmployeesByHairServicesQuery request, CancellationToken cancellationToken)
        {
            var validEmployees = await _unitOfWork.UserRepository.GetAllEmployeesByHairServicesIdsAsync(request.HairServicesId);
            if (!validEmployees.Any()) throw new NotFoundException("No employees found with the selected hair services!");
            return await Task.FromResult(validEmployees);
        }
    }
}
