using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Command.DeleteEmployeeById
{
    public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeByIdCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<Employee>> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee();
            employee.Id = request.Id;

            _employeeRepository.DeleteEmployeeAsync(employee.Id);

            Console.WriteLine("Handler -> The new list of employees:");
            var allEmployees = await _employeeRepository.GetAllEmployeesAsync();
            foreach (var empl in allEmployees)
            {
                Console.WriteLine($"id= '{empl.Id}', name= '{empl.Name}', specialization= '{empl.Specialization}'");
            }
            return await Task.FromResult(allEmployees);
        }
    }
}
