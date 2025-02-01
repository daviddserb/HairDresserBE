using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeId
{
    public class GetAllWorkingIntervalsByEmployeeIdQueryHandler : IRequestHandler<GetAllWorkingIntervalsByEmployeeIdQuery, IQueryable<WorkingInterval>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkingIntervalsByEmployeeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<WorkingInterval>> Handle(GetAllWorkingIntervalsByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var userWithRole = await _unitOfWork.UserRepository.GetUserWithRoleByIdAsync(request.EmployeeId);
            if (!userWithRole.Role.Contains("employee")) throw new NotFoundException($"The user with the '{request.EmployeeId}' id is not an employee!");

            var employeeWorkingIntervals = await _unitOfWork.WorkingIntervalRepository.GetAllWorkingIntervalsByEmployeeIdAsync(request.EmployeeId);
            if (!employeeWorkingIntervals.Any()) throw new ClientException("The employee has no working intervals!");
            return employeeWorkingIntervals;
        }
    }
}
