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
    public class AddEmployeeHairServicesCommandHandler : IRequestHandler<AddEmployeeHairServicesCommand, ApplicationUser>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddEmployeeHairServicesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApplicationUser> Handle(AddEmployeeHairServicesCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (employee == null) return null;

            var employeeHairServices = request.HairServicesIds.Select(x => new EmployeeHairService
            {
                EmployeeId = request.EmployeeId,
                HairServiceId = x
            }).ToList();

            await _unitOfWork.EmployeeRepository.AddEmployeeHairServicesAsync(employeeHairServices);
            await _unitOfWork.SaveAsync();

            return employee;
        }
    }
}
