using hairDresser.Domain.Models;
using hairDresser.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Queries.GetEmployeesByServices
{
    public class GetEmployeesByServicesQueryHandler : IRequestHandler<GetEmployeesByServicesQuery, IQueryable<Employee>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeesByServicesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Employee>> Handle(GetEmployeesByServicesQuery request, CancellationToken cancellationToken)
        {
            var validEmployees = await _unitOfWork.EmployeeRepository.GetAllEmployeesByServicesAsync(request.HairServicesId);
            return await Task.FromResult(validEmployees);
        }
    }
}
