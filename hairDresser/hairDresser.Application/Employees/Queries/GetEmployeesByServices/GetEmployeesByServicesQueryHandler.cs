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
    public class GetEmployeesByServicesQueryHandler : IRequestHandler<GetEmployeesByServicesQuery, IEnumerable<Employee>>
    {
        private IEmployeeRepository _employeeRepository;

        public GetEmployeesByServicesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetEmployeesByServicesQuery request, CancellationToken cancellationToken)
        {
            var validEmployees = await _employeeRepository.GetEmployeesByServicesAsync(request.HairServicesPickedByCustomer);
            return await Task.FromResult(validEmployees);
        }
    }
}
