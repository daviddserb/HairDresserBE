using hairDresser.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Commands.DeleteEmployeeHairService
{
    public class DeleteEmployeeHairServiceCommandHandler : IRequestHandler<DeleteEmployeeHairServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeHairServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteEmployeeHairServiceCommand request, CancellationToken cancellationToken)
        {
            var employeeHairService = await _unitOfWork.UserRepository.CheckIfEmployeeHairServiceIdExistsAsync(request.EmployeeHairServiceId);
            if (employeeHairService == null) return Unit.Value;

            await _unitOfWork.UserRepository.DeleteHairServiceFromEmployeeAsync(employeeHairService.Id);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
