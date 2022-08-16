using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            Console.Write("Handler -> All employees:\n");
            foreach (var er in _employeeRepository.GetAllEmployees())
            {
                Console.WriteLine($"id= '{er.Id}', name= '{er.Name}', specialization= '{er.Specialization}'");
            }
            return Task.FromResult(_employeeRepository.GetAllEmployees());
        }
    }
}
