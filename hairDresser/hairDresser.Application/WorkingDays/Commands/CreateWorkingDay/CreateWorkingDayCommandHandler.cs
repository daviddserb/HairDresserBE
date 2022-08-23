using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingDays.Commands.CreateWorkingDay
{
    public class CreateWorkingDayCommandHandler : IRequestHandler<CreateWorkingDayCommand>
    {
        private readonly IWorkingDayRepository _workingDayRepository;

        public CreateWorkingDayCommandHandler(IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }

        public async Task<Unit> Handle(CreateWorkingDayCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle:");

            var employeeId = request.EmployeeId;
            var startTime = TimeSpan.Parse(request.StartTime);
            var endTime = TimeSpan.Parse(request.EndTime);

            // Validation for the selected interval (startTime -> endTime) to don't overlap other intervals. And to be at least 1 hour between all working intervals.
            Console.WriteLine("Check if the Interval (startTime -> endTime) is not overlapping with other intervals:");
            var allEmployeeWorkingIntervals = await _workingDayRepository.GetWorkingDayAsync(employeeId, request.DayId);
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
                var workingDay = new WorkingDay()
                {
                    DayId = request.DayId,
                    EmployeeId = employeeId,
                    StartTime = startTime,
                    EndTime = endTime,
                };

                await _workingDayRepository.CreateWorkingDayAsync(workingDay);
            }

            return Unit.Value;
        }
    }
}
