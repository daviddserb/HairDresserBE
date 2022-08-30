using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(request.Id);

            if (employee == null) return null;

            employee.EmployeeHairServices = request.HairServicesIds.Select(hsi => new EmployeeHairService()
            {
                HairServiceId = hsi
            }).ToList();
            employee.Name = request.Name;

            await _unitOfWork.EmployeeRepository.UpdateEmployeeAsync(employee);
            await _unitOfWork.SaveAsync();

            return employee;
        }
    }
}
