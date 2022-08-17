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
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWorkingDayRepository _workingDayRepository;

        public GetEmployeeIntervalsByDateQueryHandler(IAppointmentRepository appointmentRepository, IEmployeeRepository employeeRepository, IWorkingDayRepository workingDayRepository)
        {
            _appointmentRepository = appointmentRepository;
            _employeeRepository = employeeRepository;
            _workingDayRepository = workingDayRepository;
        }

        public async Task<List<(DateTime startDate, DateTime endDate)>> Handle(GetEmployeeIntervalsByDateQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler ->");

            var employeeById = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
            var employeeName = employeeById.Name;
            Console.WriteLine("employeeName= " + employeeName);

            var appointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, request.Date);
            Console.WriteLine("appointmentDate (putem ignora ora setata, nu este importanta): " + appointmentDate);

            var duration = TimeSpan.FromMinutes(request.DurationInMinutes);
            Console.WriteLine("duration (from minutes transformed in TimeSpan): " + duration);

            var employeeAppointmentsDates = new List<(DateTime startDate, DateTime endDate)>();
            Console.WriteLine("\nAll about the appointments from the selected employee and the selected day:");
            foreach (var app in await _appointmentRepository.GetInWorkAppointmentsAsync(employeeName, appointmentDate))
            {
                employeeAppointmentsDates.Add((app.StartDate, app.EndDate));
                Console.WriteLine($"customer= '{app.CustomerName}', employee= '{app.EmployeeName}', services= '{app.HairServices}', start= '{app.StartDate}', end= '{app.EndDate}'");
            }

            var sorted_employeeAppointments = employeeAppointmentsDates.OrderBy(s => s.Item1);
            Console.WriteLine("Only the dates sorted:");
            foreach (var sapp in sorted_employeeAppointments)
            {
                Console.WriteLine(sapp.startDate + " - " + sapp.endDate);
            }

            var nameOfDay = appointmentDate.ToString("dddd");
            Console.WriteLine($"The name of the day based on the selected date is= '{nameOfDay}'");

            var timeOfDay = await _workingDayRepository.GetWorkingDayAsync(nameOfDay);
            Console.WriteLine($"start= '{timeOfDay.StartTime}', end= '{timeOfDay.EndTime}'");
            var timeOfDay_withDate_start = appointmentDate.Add(timeOfDay.StartTime);
            var timeOfDay_withDate_end = appointmentDate.Add(timeOfDay.EndTime);

            var possibleIntervals = new List<DateTime>();
            possibleIntervals.Add(timeOfDay_withDate_start);
            foreach (var sapp in sorted_employeeAppointments)
            {
                possibleIntervals.Add(sapp.startDate);
                possibleIntervals.Add(sapp.endDate);
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

            return await Task.FromResult(validIntervals);
        }
    }
}
