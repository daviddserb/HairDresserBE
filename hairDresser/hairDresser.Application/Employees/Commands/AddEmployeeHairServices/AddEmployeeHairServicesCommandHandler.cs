using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.AddEmployeeHairServices
{
    public class AddEmployeeHairServicesCommandHandler : IRequestHandler<AddEmployeeHairServicesCommand, Employee>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddEmployeeHairServicesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Employee> Handle(AddEmployeeHairServicesCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(request.EmployeeId);

            if (employee == null) return null;

            // 1
            employee.EmployeeHairServices = request.HairServicesIds.Select(hsi => new EmployeeHairService()
            {
                EmployeeId = request.EmployeeId, // ??? Nu cred ca mai trebuie...
                HairServiceId = hsi
            }).ToList();

            // 2
            //foreach (int hairServiceId in request.HairServicesIds)
            //{
            //    //employee.EmployeeHairServices.Add(hairServiceId);
            //}

            await _unitOfWork.EmployeeRepository.UpdateEmployeeHairServicesAsync(employee);
            await _unitOfWork.SaveAsync();

            return employee;
        }
    }
}
