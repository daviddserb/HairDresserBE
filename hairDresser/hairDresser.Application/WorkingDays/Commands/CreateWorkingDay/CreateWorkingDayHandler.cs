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
    public class CreateWorkingDayHandler : IRequestHandler<CreateWorkingDayQuery>
    {
        private IWorkingDayRepository _workingDayRepository;

        public CreateWorkingDayHandler(IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }

        public async Task<Unit> Handle(CreateWorkingDayQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle ->");

            var employeeId = request.EmployeeId;
            var nameOfDay = request.NameOfDay;
            var startTime = TimeSpan.Parse(request.StartTime);
            var endTime = TimeSpan.Parse(request.EndTime);

            Console.WriteLine("Check if the Interval (startTime - endTime) is not overlapping with other intervals:");
            var allEmployeeIntervalsInSelectedDay = await _workingDayRepository.GetWorkingDayAsync(employeeId, nameOfDay);
            var isIntervalGood = true;
            foreach (var intervals in allEmployeeIntervalsInSelectedDay)
            {
                Console.WriteLine("Existing interval -> " + intervals.StartTime + " - " + intervals.EndTime);
                // ??? -/+ new TimeSpan(01, 00, 00) -> ca sa fie pauza de cel putin o ora intre intervalele de lucru.Trebuie sa testez daca este corect.
                //bool overlap = startTime <= intervals.EndTime && intervals.StartTime <= endTime;
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
                    EmployeeId = employeeId,
                    Name = nameOfDay,
                    StartTime = startTime,
                    EndTime = endTime,
                };

                await _workingDayRepository.CreateWorkingDayAsync(workingDay);
            }

            return Unit.Value;
        }
    }
}
