using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class InMemoryWorkingIntervalRepository 
    {
        private readonly List<WorkingInterval> _workingDays = new();

        public InMemoryWorkingIntervalRepository()
        {
            //_workingDays.Add(new WorkingDay { Id = 1, DayId = 1, EmployeeId = 1, Name = "Monday", StartTime = new TimeSpan(08, 00, 00), EndTime = new TimeSpan(12, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 2, DayId = 1, EmployeeId = 1, Name = "Monday", StartTime = new TimeSpan(13, 00, 00), EndTime = new TimeSpan(19, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 3, DayId = 1, EmployeeId = 2, Name = "Monday", StartTime = new TimeSpan(06, 00, 00), EndTime = new TimeSpan(10, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 4, DayId = 1, EmployeeId = 2, Name = "Monday", StartTime = new TimeSpan(11, 00, 00), EndTime = new TimeSpan(17, 00, 00) });

            //_workingDays.Add(new WorkingDay { Id = 5, DayId = 2, EmployeeId = 1, Name = "Tuesday", StartTime = new TimeSpan(08, 30, 00), EndTime = new TimeSpan(12, 30, 00) });
            //_workingDays.Add(new WorkingDay { Id = 6, DayId = 2, EmployeeId = 1, Name = "Tuesday", StartTime = new TimeSpan(13, 30, 00), EndTime = new TimeSpan(19, 30, 00) });
            //_workingDays.Add(new WorkingDay { Id = 7, DayId = 2, EmployeeId = 2, Name = "Tuesday", StartTime = new TimeSpan(06, 30, 00), EndTime = new TimeSpan(10, 30, 00) });
            //_workingDays.Add(new WorkingDay { Id = 8, DayId = 2, EmployeeId = 2, Name = "Tuesday", StartTime = new TimeSpan(11, 30, 00), EndTime = new TimeSpan(17, 30, 00) });

            //_workingDays.Add(new WorkingDay { Id = 9, DayId = 3, EmployeeId = 1, Name = "Wednesday", StartTime = new TimeSpan(09, 00, 00), EndTime = new TimeSpan(13, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 10, DayId = 3, EmployeeId = 1, Name = "Wednesday", StartTime = new TimeSpan(14, 00, 00), EndTime = new TimeSpan(20, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 11, DayId = 3, EmployeeId = 2, Name = "Wednesday", StartTime = new TimeSpan(07, 00, 00), EndTime = new TimeSpan(11, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 12, DayId = 3, EmployeeId = 2, Name = "Wednesday", StartTime = new TimeSpan(12, 00, 00), EndTime = new TimeSpan(18, 00, 00) });

            //_workingDays.Add(new WorkingDay { Id = 13, DayId = 4, EmployeeId = 1, Name = "Thursday", StartTime = new TimeSpan(09, 30, 00), EndTime = new TimeSpan(13, 30, 00) });
            //_workingDays.Add(new WorkingDay { Id = 14, DayId = 4, EmployeeId = 1, Name = "Thursday", StartTime = new TimeSpan(14, 30, 00), EndTime = new TimeSpan(20, 30, 00) });
            //_workingDays.Add(new WorkingDay { Id = 15, DayId = 4, EmployeeId = 2, Name = "Thursday", StartTime = new TimeSpan(07, 30, 00), EndTime = new TimeSpan(11, 30, 00) });
            //_workingDays.Add(new WorkingDay { Id = 16, DayId = 4, EmployeeId = 2, Name = "Thursday", StartTime = new TimeSpan(12, 30, 00), EndTime = new TimeSpan(18, 30, 00) });

            //_workingDays.Add(new WorkingDay { Id = 17, DayId = 5, EmployeeId = 1, Name = "Friday", StartTime = new TimeSpan(10, 00, 00), EndTime = new TimeSpan(14, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 18, DayId = 5, EmployeeId = 1, Name = "Friday", StartTime = new TimeSpan(15, 00, 00), EndTime = new TimeSpan(21, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 19, DayId = 5, EmployeeId = 2, Name = "Friday", StartTime = new TimeSpan(08, 00, 00), EndTime = new TimeSpan(12, 00, 00) });
            //_workingDays.Add(new WorkingDay { Id = 20, DayId = 5, EmployeeId = 2, Name = "Friday", StartTime = new TimeSpan(13, 00, 00), EndTime = new TimeSpan(19, 00, 00) });

        }

        public async Task CreateWorkingDayAsync(WorkingInterval workingDay)
        {
            _workingDays.Add(workingDay);
        }

        public async Task<WorkingInterval> GetWorkingDayAsync(int dayId)
        {
            throw new NotImplementedException();
            //Console.WriteLine("Repository, day = " + _workingDays.FirstOrDefault(obj => obj.DayId == dayId));
            //return _workingDays.FirstOrDefault(obj => obj.DayId == dayId);
        }
        
        public async Task<IEnumerable<WorkingInterval>> GetWorkingDayAsync(int employeeId, string nameOfDay)
        {
            throw new NotImplementedException();
            //return _workingDays
            //.Where(obj => obj.EmployeeId == employeeId)
            //.Where(obj => obj.DayId == 3);
        }

        public async Task<IEnumerable<WorkingInterval>> ReadWorkingDaysAsync()
        {
            return _workingDays;
        }

        public async Task UpdateWorkingDayAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteWorkingDayAsync()
        {
            throw new NotImplementedException();
        }
    }
}