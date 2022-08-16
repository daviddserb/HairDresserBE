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
        public Task<IEnumerable<Employee>> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee();
            employee.Id = request.Id;

            _employeeRepository.DeleteEmployee(employee.Id);

            Console.WriteLine("Handler -> The new list of employees:");
            foreach (var er in _employeeRepository.GetAllEmployees())
            {
                Console.WriteLine($"id= '{er.Id}', name= '{er.Name}', specialization= '{er.Specialization}'");
            }
            return Task.FromResult(_employeeRepository.GetAllEmployees());
        }
    }
}
