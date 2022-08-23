using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.DeleteEmployeeById
{
    // public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, Unit> -> este la fel daca sau nu declari Unit, care este un tip de void.
    public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeByIdCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle:");
            // ???!!! Sa vad daca am nevoie de employee
            var employee = new Employee
            {
                Id = request.Id
            };

            await _employeeRepository.DeleteEmployeeAsync(employee.Id);
            return Unit.Value;
        }
    }
}
