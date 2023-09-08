using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Commands.CreateWorkingInterval
{
    public class CreateWorkingIntervalCommandHandler : IRequestHandler<CreateWorkingIntervalCommand, WorkingInterval>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkingIntervalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkingInterval> Handle(CreateWorkingIntervalCommand request, CancellationToken cancellationToken)
        {
            var userWithRole = await _unitOfWork.UserRepository.GetUserWithRoleByIdAsync(request.EmployeeId);
            if (!userWithRole.Role.Contains("employee")) throw new NotFoundException($"The user with the '{request.EmployeeId}' id is not an employee!");

            TimeSpan startTime = TimeSpan.Parse(request.StartTime);
            TimeSpan endTime = TimeSpan.Parse(request.EndTime);

            if (startTime > endTime) throw new ClientException($"The working interval start time '{startTime}' needs to be before the end time '{endTime}'!");

            TimeSpan workingIntervalMinimumDuration = new TimeSpan(04, 00, 00);
            if (endTime - startTime < workingIntervalMinimumDuration) throw new ClientException($"The working interval minimum duration is {workingIntervalMinimumDuration}!");

            var employeeWorkingIntervals = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(request.EmployeeId, request.WorkingDayId);
            foreach (var employeeInterval in employeeWorkingIntervals)
            {
                // Check the new working interval to don't overlap with the existing ones and to have at least one hour pause between them.
                TimeSpan minimumDurationBetweenWorkingIntervals = new TimeSpan(01, 00, 00);
                bool overlap = startTime < employeeInterval.EndTime + minimumDurationBetweenWorkingIntervals && employeeInterval.StartTime - minimumDurationBetweenWorkingIntervals < endTime;
                if (overlap) throw new ClientException($"The working interval ({employeeInterval.StartTime} - {employeeInterval.EndTime}) is overlapping or the pause between the working intervals isn't at least {minimumDurationBetweenWorkingIntervals}!");
            }

            var workingInterval = new WorkingInterval()
            {
                WorkingDayId = request.WorkingDayId,
                EmployeeId = request.EmployeeId,
                StartTime = startTime,
                EndTime = endTime,
            };

            await _unitOfWork.WorkingIntervalRepository.CreateWorkingIntervalAsync(workingInterval);
            await _unitOfWork.SaveAsync();

            return await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalByIdAsync(workingInterval.Id);
        }
    }
}