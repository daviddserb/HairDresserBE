using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Commands.UpdateWorkingInterval
{
    public class UpdateWorkingIntervalCommandHandler : IRequestHandler<UpdateWorkingIntervalCommand, WorkingInterval>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkingIntervalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkingInterval> Handle(UpdateWorkingIntervalCommand request, CancellationToken cancellationToken)
        {
            var workingInterval = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalByIdAsync(request.WorkingIntervalId);
            if (workingInterval == null) throw new NotFoundException($"There is no working interval registered with the id '{request.WorkingIntervalId}'!");

            TimeSpan startTime = TimeSpan.Parse(request.StartTime);
            TimeSpan endTime = TimeSpan.Parse(request.EndTime);

            if (startTime > endTime) throw new ClientException($"The working interval start time '{startTime}' needs to be before the end time '{endTime}'!");

            TimeSpan workingIntervalMinimumDuration = new TimeSpan(04, 00, 00);
            if (endTime - startTime < workingIntervalMinimumDuration) throw new ClientException($"The working interval minimum duration is {workingIntervalMinimumDuration}!");

            var employeeWorkingIntervals = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(workingInterval.EmployeeId, request.WorkingDayId);

            // Exclude the working interval that is selected to be updated.
            var updatedEmployeeWorkingIntervals = employeeWorkingIntervals
                .Where(interval => interval.Id != request.WorkingIntervalId)
                .ToList();

            foreach (var employeeInterval in updatedEmployeeWorkingIntervals)
            {
                // Check the new working interval to don't overlap with the existing ones and to have at least one hour pause between them.
                TimeSpan minimumDurationBetweenWorkingIntervals = new TimeSpan(01, 00, 00);
                bool overlap = startTime < employeeInterval.EndTime + minimumDurationBetweenWorkingIntervals && employeeInterval.StartTime - minimumDurationBetweenWorkingIntervals < endTime;
                if (overlap) throw new ClientException($"The working interval ({employeeInterval.StartTime} - {employeeInterval.EndTime}) is overlapping or the pause between the working intervals isn't at least {minimumDurationBetweenWorkingIntervals}!");
            }

            workingInterval.WorkingDayId = request.WorkingDayId;
            workingInterval.StartTime = TimeSpan.Parse(request.StartTime);
            workingInterval.EndTime = TimeSpan.Parse(request.EndTime);

            await _unitOfWork.WorkingIntervalRepository.UpdateWorkingIntervalAsync(workingInterval);
            await _unitOfWork.SaveAsync();

            return workingInterval;
        }
    }
}