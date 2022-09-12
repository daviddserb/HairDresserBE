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
    public class CreateEmployeeComandHandler : IRequestHandler<CreateEmployeeComand, Employee>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeComandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> Handle(CreateEmployeeComand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = request.Name,
                EmployeeHairServices = request.HairServicesIds.Select(hsi => new EmployeeHairService()
                {
                    HairServiceId = hsi
                }).ToList()
            };

            await _unitOfWork.EmployeeRepository.CreateEmployeeAsync(employee);
            await _unitOfWork.SaveAsync();

            var createdEmployee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(employee.Id);

            return createdEmployee;
        }
    }
}
