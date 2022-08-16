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

        public Task<IEnumerable<Employee>> Handle(GetEmployeesByServicesQuery request, CancellationToken cancellationToken)
        {
            Console.Write("Handler -> All the employees:\n");
            foreach (var employee in _employeeRepository.GetAllEmployees())
            {
                Console.WriteLine($"name= '{employee.Name}', specialization= '{employee.Specialization}'");
            }

            return Task.FromResult(_employeeRepository.GetEmployeesByServices(request.HairServicesPickedByCustomer));
        }
    }
}
