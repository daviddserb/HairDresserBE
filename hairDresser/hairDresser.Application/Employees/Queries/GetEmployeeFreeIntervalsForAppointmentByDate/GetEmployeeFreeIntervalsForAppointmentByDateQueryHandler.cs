using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Queries.GetEmployeeFreeIntervalsForAppointmentByDate
{
    public class GetEmployeeFreeIntervalsForAppointmentByDateQueryHandler : IRequestHandler<GetEmployeeFreeIntervalsForAppointmentByDateQuery, List<EmployeeFreeInterval>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeeFreeIntervalsForAppointmentByDateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeFreeInterval>> Handle(GetEmployeeFreeIntervalsForAppointmentByDateQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("\nGetEmployeeIntervalsByDateQueryHandler:");

            var maxAppointmentsCustomerPerMonth = 5;
            var customerAppointmentsLastMonth = await _unitOfWork.AppointmentRepository.GetHowManyAppointmentsCustomerHasInLastMonth(request.CustomerId);
            if (customerAppointmentsLastMonth > maxAppointmentsCustomerPerMonth) throw new ClientException("You went over the limit of maximum appointments for this month!");

            var appointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, request.Date);
            Console.WriteLine($"appointment (ignoram Time-ul, am setat doar Date-ul, adica Day, in Anul curent si Luna curenta)= '{appointmentDate}'");

            var duration = TimeSpan.FromMinutes(request.DurationInMinutes);
            Console.WriteLine($"\nduration, from minutes to TimeSpan,= '{duration}'");

            var employeeAppointmentsDates = new List<(DateTime startDate, DateTime endDate)>();
            Console.WriteLine("\nAll the appointments from the selected employee and the selected date:");
            foreach (var appointment in await _unitOfWork.AppointmentRepository.GetAllAppointmentsByEmployeeIdByDateAsync(request.EmployeeId, appointmentDate))
            {
                employeeAppointmentsDates.Add((appointment.StartDate, appointment.EndDate));
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

            var workingDay = await _unitOfWork.WorkingDayRepository.GetWorkingDayByNameAsync(nameOfDay);

            var employeeWorkingIntervals = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(request.EmployeeId, workingDay.Id);
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

            // Am lista asta doar de testare a intervalelor. !!! Ca sa scap de ea, as putea sa parsez lista de EmployeeFreeInterval, dar trebuie sa invat sa vad cum fac asta.
            //var validIntervals = new List<(DateTime startDate, DateTime endDate)>();
            var employeeFreeIntervalList = new List<EmployeeFreeInterval>();
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
                    //validIntervals.Add((copy_startOfInterval, startOfInterval));
                    employeeFreeIntervalList.Add(new EmployeeFreeInterval { StartDate = copy_startOfInterval, EndDate = startOfInterval });
                    copy_startOfInterval = startOfInterval;
                }
            }
            Console.WriteLine("\nEMPLOYEE FREE INTERVALS FOR APPOINTMENT:");
            foreach (var employeeFreeInterval in employeeFreeIntervalList)
            {
                Console.WriteLine($"start= '{employeeFreeInterval.StartDate}', end= '{employeeFreeInterval.EndDate}'");
            }

            var customerAppointmentsInSelectedDate = await _unitOfWork.AppointmentRepository.GetAllAppointmentsByCustomerIdByDateAsync(request.CustomerId, appointmentDate);
            Console.WriteLine("\nCUSTOMER CURRENT APPOINTMENTS:");
            foreach (var customerAppointments in customerAppointmentsInSelectedDate)
            {
                Console.WriteLine($"start= '{customerAppointments.StartDate}', end= '{customerAppointments.EndDate}'");
            }

            // Check, all the possible free intervals from the employee in the selected date, to don't overlap with the appointments from the customer in the selected date.
            for (int i = 0; i < employeeFreeIntervalList.Count; ++i)
            {
                //Console.WriteLine("i= " + i);
                //Console.WriteLine($"EMPLOYEE: start= '{employeeFreeIntervalList[i].StartDate}', end= '{employeeFreeIntervalList[i].EndDate}'");
                foreach (var customerAppointments in customerAppointmentsInSelectedDate)
                {
                    //Console.WriteLine($"CUSTOMER: start= '{customerAppointments.StartDate}', end= '{customerAppointments.EndDate}'");
                    bool overlap = employeeFreeIntervalList[i].StartDate < customerAppointments.EndDate && customerAppointments.StartDate < employeeFreeIntervalList[i].EndDate;
                    //Console.WriteLine("overlap= " + overlap);
                    if (overlap)
                    {
                        employeeFreeIntervalList.RemoveAt(i);
                        --i; // ! Because otherwise it will jump over the next element when it will remove the current one (think about how the algorithm of removing works and it makes sense).
                        break;
                    }
                }
                //Console.WriteLine("---\n");
            }

            Console.WriteLine("\nFINAL VALID INTERVALS:");
            foreach (var employeeFreeInterval in employeeFreeIntervalList)
            {
                Console.WriteLine($"start= '{employeeFreeInterval.StartDate}', end= '{employeeFreeInterval.EndDate}'");
            }

            return await Task.FromResult(employeeFreeIntervalList);
        }
    }
}