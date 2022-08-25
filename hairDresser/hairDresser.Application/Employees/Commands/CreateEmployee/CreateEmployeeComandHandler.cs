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
            var employee = new Employee
            {
                Name = request.Name,
                EmployeeHairServices = request.SpecializationsIds.Select(hsi => new EmployeeHairService()
                {
                    HairServiceId = hsi
                }).ToList()
            };
            await _employeeRepository.CreateEmployeeAsync(employee);
            return Unit.Value;
        }
    }
}
