using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.DeleteEmployee
{
    // public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, Unit> -> este la fel daca sau nu declari Unit, care este un tip de void.
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Employee>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(request.Id);

            if (employee == null) return null;

            await _unitOfWork.EmployeeRepository.DeleteEmployeeAsync(employee.Id);
            await _unitOfWork.SaveAsync();
            return employee;
        }
    }
}
