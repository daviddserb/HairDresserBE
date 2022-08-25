using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Queries.GetEmployeeIntervalsForAppointmentByDate
{
    public class GetEmployeeIntervalsByDateQueryHandler : IRequestHandler<GetEmployeeIntervalsByDateQuery, List<(DateTime startDate, DateTime endDate)>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDayRepository _dayRepository;
        private readonly IWorkingDayRepository _workingDayRepository;

        public GetEmployeeIntervalsByDateQueryHandler(IAppointmentRepository appointmentRepository, IEmployeeRepository employeeRepository, IDayRepository dayRepository, IWorkingDayRepository workingDayRepository)
        {
            _employeeRepository = employeeRepository;
            _appointmentRepository = appointmentRepository;
            _dayRepository = dayRepository;
            _workingDayRepository = workingDayRepository;
        }

        public async Task<List<(DateTime startDate, DateTime endDate)>> Handle(GetEmployeeIntervalsByDateQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("GetEmployeeIntervalsByDateQueryHandler:");

            var employee = await _employeeRepository.GetEmployeeAsync(request.EmployeeId);
            Console.WriteLine($"employeeName= '{employee.Name}'");

            var appointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, request.Date);
            Console.WriteLine($"appointmentDate (ignoram Time-ul, am setat doar Date-ul (Day) in Anul curent si Luna curenta= '{appointmentDate}'");

            var duration = TimeSpan.FromMinutes(request.DurationInMinutes);
            Console.WriteLine($"duration (from minutes transformed in TimeSpan)== '{duration}'");

            var employeeAppointmentsDates = new List<(DateTime startDate, DateTime endDate)>();
            Console.WriteLine("\nAll about the appointments from the selected employee and the selected date:");
            foreach (var app in await _appointmentRepository.GetAppointmentsInWorkAsync(employee.Name, appointmentDate))
            {
                employeeAppointmentsDates.Add((app.StartDate, app.EndDate));
                //???
                //Console.WriteLine($"customer= '{app.CustomerName}', employee= '{app.EmployeeName}', services= '{app.HairServices}', start= '{app.StartDate}', end= '{app.EndDate}'");
            }

            var nameOfDay = appointmentDate.ToString("dddd");
            Console.WriteLine($"\nname of the day based on the selected date is= '{nameOfDay}'");

            var day = await _dayRepository.GetDayAsync(nameOfDay);
            Console.WriteLine($"day: Id= '{day.Id}', Name= '{day.Name}'");

            var employeeWorkingIntervals = await _workingDayRepository.GetWorkingDayAsync(request.EmployeeId, day.Id);
            var possibleIntervals = new List<DateTime>();
            foreach (var intervals in employeeWorkingIntervals)
            {
                Console.WriteLine($"day intervals (start - end): '{intervals.StartTime}' - '{intervals.EndTime}'");
                possibleIntervals.Add(appointmentDate.Add(intervals.StartTime));
                foreach (var app in employeeAppointmentsDates)
                {
                    if (intervals.StartTime <= app.startDate.TimeOfDay && app.endDate.TimeOfDay <= intervals.EndTime)
                    {
                        possibleIntervals.Add(app.startDate);
                        possibleIntervals.Add(app.endDate);
                    }
                }
                possibleIntervals.Add(appointmentDate.Add(intervals.EndTime));
            }
            Console.WriteLine("\nPossible Intervals:");
            foreach (var intervals in possibleIntervals)
            {
                Console.WriteLine(intervals);
            }

            var validIntervals = new List<(DateTime startDate, DateTime endDate)>();
            Console.WriteLine("\nCheck for valid intervals:");
            for (int i = 0; i < possibleIntervals.Count - 1; i += 2)
            {
                var startOfInterval = possibleIntervals[i];
                var copy_startOfInterval = startOfInterval;
                var endOfInterval = possibleIntervals[i + 1];
                Console.WriteLine($"intervals (start - end): '{startOfInterval.TimeOfDay}' - '{endOfInterval.TimeOfDay}'");
                Console.WriteLine("Valid dates:");
                while ((startOfInterval += duration) <= endOfInterval)
                {
                    Console.WriteLine(copy_startOfInterval.TimeOfDay + " - " + startOfInterval.TimeOfDay);
                    validIntervals.Add((copy_startOfInterval, startOfInterval));
                    copy_startOfInterval = startOfInterval;
                }
            }
            //Console.WriteLine("\nAll valid intervals:");
            //foreach (var intervals in validIntervals)
            //{
            //    Console.WriteLine($"start= '{intervals.startDate}', end= '{intervals.endDate}'");
            //}
            return await Task.FromResult(validIntervals);
        }
    }
}