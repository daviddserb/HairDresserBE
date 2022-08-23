using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeComandHandler : IRequestHandler<CreateEmployeeComand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeComandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(CreateEmployeeComand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle:");
            var employee = new Employee();

            employee.Name = request.Name;

            for (int i = 0; i < request.Specializations.Count; ++i)
            {
                employee.Specialization += request.Specializations[i];
                if (i != request.Specializations.Count - 1)
                {
                    employee.Specialization += ", ";
                }
            }

            await _employeeRepository.CreateEmployeeAsync(employee);

            return Unit.Value;
        }
    }
}
