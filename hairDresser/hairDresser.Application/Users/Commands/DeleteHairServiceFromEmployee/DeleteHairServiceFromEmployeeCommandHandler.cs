using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Commands.DeleteEmployeeHairService
{
    public class DeleteHairServiceFromEmployeeCommandHandler : IRequestHandler<DeleteHairServiceFromEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHairServiceFromEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteHairServiceFromEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeHairService = await _unitOfWork.UserRepository.CheckIfEmployeeHairServiceIdExistsAsync(request.EmployeeHairServiceId);
            if (employeeHairService == null) throw new NotFoundException("The selected employee does not have the selected hair service!");

            await _unitOfWork.UserRepository.DeleteHairServiceFromEmployeeAsync(employeeHairService.Id);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
