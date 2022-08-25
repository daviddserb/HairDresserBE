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
    public class CreateWorkingIntervalCommandHandler : IRequestHandler<CreateWorkingIntervalCommand>
    {
        private readonly IWorkingIntervalRepository _workingIntervalRepository;

        public CreateWorkingIntervalCommandHandler(IWorkingIntervalRepository workingIntervalRepository)
        {
            _workingIntervalRepository = workingIntervalRepository;
        }

        public async Task<Unit> Handle(CreateWorkingIntervalCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle:");

            var startTime = TimeSpan.Parse(request.StartTime);
            var endTime = TimeSpan.Parse(request.EndTime);

            // Validation for the selected interval (startTime -> endTime) to don't overlap other intervals and to be at least 1 hour between all working intervals.
            Console.WriteLine("Check if the Interval (startTime -> endTime) is not overlapping with other intervals:");
            var allEmployeeWorkingIntervals = await _workingIntervalRepository.GetWorkingIntervalByEmployeeIdByWorkingDayIdAsync(request.EmployeeId, request.DayId);
            var isIntervalGood = true;
            foreach (var intervals in allEmployeeWorkingIntervals)
            {
                Console.WriteLine("Existing interval -> " + intervals.StartTime + " - " + intervals.EndTime);
                bool overlap = startTime < intervals.EndTime + new TimeSpan(01, 00, 00) && intervals.StartTime - new TimeSpan(01, 00, 00) < endTime;
                Console.WriteLine("overlap= " + overlap);
                if (overlap)
                {
                    isIntervalGood = false;
                    Console.WriteLine("Interval overlapping");
                    break;
                }
            }
            if (isIntervalGood == true)
            {
                Console.WriteLine("INTERVAL NOT OVERLAPPING");
                var workingInterval = new WorkingInterval()
                {
                    WorkingDayId = request.DayId,
                    EmployeeId = request.EmployeeId,
                    StartTime = startTime,
                    EndTime = endTime,
                };
                await _workingIntervalRepository.CreateWorkingIntervalAsync(workingInterval);
            }

            return Unit.Value;
        }
    }
}
