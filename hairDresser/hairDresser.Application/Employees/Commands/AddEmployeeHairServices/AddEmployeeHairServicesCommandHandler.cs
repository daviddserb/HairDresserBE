using hairDresser.Application.CustomExceptions;
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
    public class AddEmployeeHairServicesCommandHandler : IRequestHandler<AddEmployeeHairServicesCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddEmployeeHairServicesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(AddEmployeeHairServicesCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (user == null) throw new NotFoundException($"The user with the id '{request.EmployeeId}' does not exist!");

            var employeeHairServices = request.HairServicesIds.Select(x => new EmployeeHairService
            {
                EmployeeId = request.EmployeeId,
                HairServiceId = x
            }).ToList();

            await _unitOfWork.EmployeeRepository.AddEmployeeHairServicesAsync(employeeHairServices);
            await _unitOfWork.SaveAsync();

            return user;
        }
    }
}
