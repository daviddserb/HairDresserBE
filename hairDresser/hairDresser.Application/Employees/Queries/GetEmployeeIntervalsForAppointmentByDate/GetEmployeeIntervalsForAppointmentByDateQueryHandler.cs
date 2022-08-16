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
    public class GetEmployeeIntervalsForAppointmentByDateQueryHandler : IRequestHandler<GetEmployeeIntervalsForAppointmentByDateQuery, List<(DateTime startDate, DateTime endDate)>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWorkingDayRepository _workingDayRepository;

        public GetEmployeeIntervalsForAppointmentByDateQueryHandler(IAppointmentRepository appointmentRepository, IEmployeeRepository employeeRepository, IWorkingDayRepository workingDayRepository)
        {
            _appointmentRepository = appointmentRepository;
            _employeeRepository = employeeRepository;
            _workingDayRepository = workingDayRepository;
        }

        public Task<List<(DateTime startDate, DateTime endDate)>> Handle(GetEmployeeIntervalsForAppointmentByDateQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler ->");

            var employeeName = _employeeRepository.GetEmployeeById(request.EmployeeId).Name;
            Console.WriteLine("employeeName= " + employeeName);

            var appointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, request.Date);
            Console.WriteLine("appointmentDate= " + appointmentDate);

            var duration = TimeSpan.FromMinutes(request.DurationInMinutes);
            Console.WriteLine("duration from minutes transformed in TimeSpan= " + duration);

            var employeeAppointmentsDates = new List<(DateTime startDate, DateTime endDate)>();
            Console.WriteLine("\nAll about the appointments from the selected employee and the selected day:");
            foreach (var ar in _appointmentRepository.GetInWorkAppointments(employeeName, appointmentDate))
            {
                employeeAppointmentsDates.Add((ar.StartDate, ar.EndDate));
                Console.WriteLine($"customer= '{ar.CustomerName}', employee= '{ar.EmployeeName}', services= '{ar.HairServices}', start= '{ar.StartDate}', end= '{ar.EndDate}'");
            }

            Console.WriteLine("Only the dates:");
            foreach (var ea in employeeAppointmentsDates)
            {
                Console.WriteLine($"start= '{ea.Item1}', end= '{ea.Item2}'");
            }

            var sorted_employeeAppointments = employeeAppointmentsDates.OrderBy(s => s.Item1);
            Console.WriteLine("Only the dates sorted:");
            foreach (var sea in sorted_employeeAppointments)
            {
                Console.WriteLine(sea.startDate + " - " + sea.endDate);
            }

            var nameOfDay = appointmentDate.ToString("dddd");
            Console.WriteLine($"The name of the day based on the selected date is= '{nameOfDay}'");
            var timeOfDay = _workingDayRepository.GetWorkingDay(nameOfDay);
            Console.WriteLine($"start= '{timeOfDay.StartTime}', end= '{timeOfDay.EndTime}'");
            var timeOfDay_withDate_start = appointmentDate.Add(timeOfDay.StartTime);
            var timeOfDay_withDate_end = appointmentDate.Add(timeOfDay.EndTime);

            var possibleIntervals = new List<DateTime>();
            possibleIntervals.Add(timeOfDay_withDate_start);
            foreach (var sortedApp in sorted_employeeAppointments)
            {
                possibleIntervals.Add(sortedApp.startDate);
                possibleIntervals.Add(sortedApp.endDate);
            }
            possibleIntervals.Add(timeOfDay_withDate_end);

            Console.WriteLine("Possible intervals:");
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
                Console.WriteLine("startOfInterval= " + startOfInterval);
                Console.WriteLine("endOfInterval= " + endOfInterval);

                while ((startOfInterval += duration) <= endOfInterval)
                {
                    Console.WriteLine("Valid dates in this interval: " + copy_startOfInterval + " - " + startOfInterval);
                    validIntervals.Add((copy_startOfInterval, startOfInterval));
                    copy_startOfInterval = startOfInterval;
                }
            }

            Console.WriteLine("\nAll valid intervals:");
            foreach (var intervals in validIntervals)
            {
                Console.WriteLine($"start= '{intervals.startDate}', end= '{intervals.endDate}'");
            }

            return Task.FromResult(validIntervals);
        }
    }
}
