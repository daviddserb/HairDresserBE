using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var startTime = TimeSpan.Parse(request.StartTime);
            var endTime = TimeSpan.Parse(request.EndTime);

            // Validation for the selected interval (startTime -> endTime) to don't overlap other intervals and to be at least 1 hour between all working intervals.
            Console.WriteLine("Check if the Interval (startTime -> endTime) is not overlapping with other intervals:");
            var employeeAllWorkingIntervals = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(request.EmployeeId, request.WorkingDayId);
            var isIntervalGood = true;
            foreach (var workingInterval in employeeAllWorkingIntervals)
            {
                Console.WriteLine("Existing interval -> " + workingInterval.StartTime + " - " + workingInterval.EndTime);
                bool overlap = startTime < workingInterval.EndTime + new TimeSpan(01, 00, 00) && workingInterval.StartTime - new TimeSpan(01, 00, 00) < endTime;
                Console.WriteLine("overlap= " + overlap);
                if (overlap)
                {
                    isIntervalGood = false;
                    Console.WriteLine("INTERVAL OVERLAPPING");
                    break;
                }
            }
            if (isIntervalGood == true)
            {
                Console.WriteLine("INTERVAL NOT OVERLAPPING");
                var workingInterval = new WorkingInterval()
                {
                    WorkingDayId = request.WorkingDayId,
                    EmployeeId = request.EmployeeId,
                    StartTime = startTime,
                    EndTime = endTime,
                };

                await _unitOfWork.WorkingIntervalRepository.CreateWorkingIntervalAsync(workingInterval);
                await _unitOfWork.SaveAsync();

                var createdWorkingInterval = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalByIdAsync(workingInterval.Id);

                return createdWorkingInterval;
            }

            // Else (if the interval is not good, because it does overlap) => return null object.
            return new WorkingInterval();
        }
    }
}
