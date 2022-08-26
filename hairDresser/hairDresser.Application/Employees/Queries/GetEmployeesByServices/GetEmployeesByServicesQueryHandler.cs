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

        public GetEmployeesByServicesQueryHandler(IEmployeeRepository employeeRepository, IHairServiceRepository hairServiceRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetEmployeesByServicesQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle:");
            var validEmployees = await _employeeRepository.GetAllEmployeesByServicesAsync(request.HairServicesId);
            return await Task.FromResult(validEmployees);
        }
    }
}
