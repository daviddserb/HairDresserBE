using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Commands.AddHairServicesToEmployee
{
    public class AddHairServicesToEmployeeCommandHandler : IRequestHandler<AddHairServicesToEmployeeCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddHairServicesToEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(AddHairServicesToEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (employee == null) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' does not exist!");

            var employeeHairServices = request.HairServicesIds.Select(hairServiceId => new EmployeeHairService
            {
                EmployeeId = request.EmployeeId,
                HairServiceId = hairServiceId
            }).ToList();

            await _unitOfWork.UserRepository.AddHairServiceToEmployeeAsync(employeeHairServices);
            await _unitOfWork.SaveAsync();

            return employee;
        }
    }
}
