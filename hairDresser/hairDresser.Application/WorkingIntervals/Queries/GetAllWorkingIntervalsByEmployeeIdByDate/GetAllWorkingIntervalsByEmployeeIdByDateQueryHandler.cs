using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeIdByDate
{
    public class GetAllWorkingIntervalsByEmployeeIdByDateQueryHandler : IRequestHandler<GetAllWorkingIntervalsByEmployeeIdByDateQuery, IQueryable<WorkingInterval>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkingIntervalsByEmployeeIdByDateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<WorkingInterval>> Handle(GetAllWorkingIntervalsByEmployeeIdByDateQuery request, CancellationToken cancellationToken)
        {
            var userWithRole = await _unitOfWork.UserRepository.GetUserWithRoleByIdAsync(request.EmployeeId);
            if (!userWithRole.Role.Contains("employee")) throw new NotFoundException($"The user with the '{request.EmployeeId}' id is not an registered employee!");

            var workingDay = await _unitOfWork.WorkingDayRepository.GetWorkingDayByIdAsync(request.WorkingDayId);
            if (workingDay == null) throw new NotFoundException($"The working day with the '{request.WorkingDayId}' id is not registered!");

            var employeeWorkingIntervalsInSelectedDay = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(request.EmployeeId, request.WorkingDayId);
            if (!employeeWorkingIntervalsInSelectedDay.Any()) throw new ClientException("The employee has no working intervals registered in the selected day!");
            return employeeWorkingIntervalsInSelectedDay;
        }
    }
}
