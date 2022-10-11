using hairDresser.Application.Employees.Commands.DeleteEmployee;
using hairDresser.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.DeleteEmployeeHairService
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
            var employeeHairService = await _unitOfWork.EmployeeRepository.CheckIfEmployeeHairServiceIdExists(request.EmployeeHairServiceId);
            if (employeeHairService == null) return Unit.Value;


            await _unitOfWork.EmployeeRepository.DeleteEmployeeHairServiceAsync(employeeHairService.Id);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
