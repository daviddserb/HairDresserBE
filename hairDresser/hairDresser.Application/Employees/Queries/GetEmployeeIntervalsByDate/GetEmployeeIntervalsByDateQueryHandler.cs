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
        private readonly IWorkingDayRepository _workingDayRepository;
        private readonly IWorkingIntervalRepository _workingIntervalRepository;

        public GetEmployeeIntervalsByDateQueryHandler(IAppointmentRepository appointmentRepository, IEmployeeRepository employeeRepository, IWorkingDayRepository workingDayRepository, IWorkingIntervalRepository workingIntervalRepository)
        {
            _employeeRepository = employeeRepository;
            _appointmentRepository = appointmentRepository;
            _workingDayRepository = workingDayRepository;
            _workingIntervalRepository = workingIntervalRepository;
        }

        public async Task<List<(DateTime startDate, DateTime endDate)>> Handle(GetEmployeeIntervalsByDateQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("\nGetEmployeeIntervalsByDateQueryHandler:");

            //var employee = await _employeeRepository.GetEmployeeAsync(request.EmployeeId);
            //Console.WriteLine($"employeeName= '{employee.Name}'");

            var appointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, request.Date);
            Console.WriteLine($"appointmentDate (ignoram Time-ul, am setat doar Date-ul (Day) in Anul curent si Luna curenta= '{appointmentDate}'");

            var duration = TimeSpan.FromMinutes(request.DurationInMinutes);
            Console.WriteLine($"duration (from minutes transformed in TimeSpan)== '{duration}'");

            var employeeAppointmentsDates = new List<(DateTime startDate, DateTime endDate)>();
            Console.WriteLine("\nAll the appointments from the selected employee and the selected date:");
            foreach (var appointment in await _appointmentRepository.GetAllAppointmentsByEmployeeIdByDateAsync(request.EmployeeId, appointmentDate))
            {
                employeeAppointmentsDates.Add((appointment.StartDate, appointment.EndDate));
                // 1. Nu trebuie neaparat, nu ajuta la algoritm, dar cum fac sa vad si toate HairServices-urile de la fiecare appointment? Daca reusesc sa inteleg si sa fac asta, imi rezolv si alte probleme pe care le am in cod.
                // 2. Pe metoda GetAppointmentsInWorkAsync din AppointmentRepository, la return-ul ei am facut join pe Customer, ca aici sa pot face .Customer.Name, dar nu am facut join si pe Employee si de ce pot sa fac .Employee.Name?
                Console.WriteLine($"{appointment.Id} - customer= '{appointment.Customer.Name}', employee='{appointment.Employee.Name}', start= '{appointment.StartDate}', end= '{appointment.EndDate}'");
            }

            Console.WriteLine("\nSorted appointments:");
            var sorted_employeeAppointmentsDates = employeeAppointmentsDates.OrderBy(date => date.startDate);
            foreach (var sortedAppointment in sorted_employeeAppointmentsDates)
            {
                Console.WriteLine($"start= '{sortedAppointment.startDate}', end= '{sortedAppointment.endDate}'");
            }

            var nameOfDay = appointmentDate.ToString("dddd");
            Console.WriteLine($"\nname of the day based on the selected date is= '{nameOfDay}'");

            var workingDay = await _workingDayRepository.GetWorkingDayByNameAsync(nameOfDay);
            Console.WriteLine($"day: Id= '{workingDay.Id}', Name= '{workingDay.Name}'");

            var employeeWorkingIntervals = await _workingIntervalRepository.GetWorkingIntervalByEmployeeIdByWorkingDayIdAsync(request.EmployeeId, workingDay.Id);
            var possibleIntervals = new List<DateTime>();
            foreach (var intervals in employeeWorkingIntervals)
            {
                Console.WriteLine($"day intervals (start - end): '{intervals.StartTime}' - '{intervals.EndTime}'");
                possibleIntervals.Add(appointmentDate.Add(intervals.StartTime));
                foreach (var app in sorted_employeeAppointmentsDates)
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