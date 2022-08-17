using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Command.CreateEmployee
{
    public class CreateEmployeeComandHandler : IRequestHandler<CreateEmployeeComand, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeComandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> Handle(CreateEmployeeComand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler ->");
            var employee = new Employee();
            var allEmployees = await _employeeRepository.GetAllEmployeesAsync();
            employee.Id = allEmployees.Max(employee => employee.Id) + 1;
            employee.Name = request.Name;

            for (int i = 0; i < request.Specializations.Count; ++i)
            {
                employee.Specialization += request.Specializations[i];
                if (i != request.Specializations.Count - 1)
                {
                    employee.Specialization += ", ";
                }
            }

            _employeeRepository.CreateEmployeeAsync(employee);

            Console.WriteLine("The new list of employees:");
            foreach (var er in await _employeeRepository.GetAllEmployeesAsync())
            {
                Console.WriteLine($"id= '{er.Id}', name= '{er.Name}', specialization= '{er.Specialization}'");
            }
            return await Task.FromResult(employee);

        }
    }
}
