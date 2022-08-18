using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class WorkingDayRepository : IWorkingDayRepository
    {
        public List<WorkingDay> WorkingDayList = new();

        public WorkingDayRepository()
        {
            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Monday", StartTime = new TimeSpan(08, 00, 00), EndTime = new TimeSpan(12, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Monday", StartTime = new TimeSpan(13, 00, 00), EndTime = new TimeSpan(19, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Monday", StartTime = new TimeSpan(06, 00, 00), EndTime = new TimeSpan(10, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Monday", StartTime = new TimeSpan(11, 00, 00), EndTime = new TimeSpan(17, 00, 00) });

            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Tuesday", StartTime = new TimeSpan(08, 30, 00), EndTime = new TimeSpan(12, 30, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Tuesday", StartTime = new TimeSpan(13, 30, 00), EndTime = new TimeSpan(19, 30, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Tuesday", StartTime = new TimeSpan(06, 30, 00), EndTime = new TimeSpan(10, 30, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Tuesday", StartTime = new TimeSpan(11, 30, 00), EndTime = new TimeSpan(17, 30, 00) });

            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Wednesday", StartTime = new TimeSpan(09, 00, 00), EndTime = new TimeSpan(13, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Wednesday", StartTime = new TimeSpan(14, 00, 00), EndTime = new TimeSpan(20, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Wednesday", StartTime = new TimeSpan(07, 00, 00), EndTime = new TimeSpan(11, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Wednesday", StartTime = new TimeSpan(12, 00, 00), EndTime = new TimeSpan(18, 00, 00) });

            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Thursday", StartTime = new TimeSpan(09, 30, 00), EndTime = new TimeSpan(13, 30, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Thursday", StartTime = new TimeSpan(14, 30, 00), EndTime = new TimeSpan(20, 30, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Thursday", StartTime = new TimeSpan(07, 30, 00), EndTime = new TimeSpan(11, 30, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Thursday", StartTime = new TimeSpan(12, 30, 00), EndTime = new TimeSpan(18, 30, 00) });

            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Friday", StartTime = new TimeSpan(10, 00, 00), EndTime = new TimeSpan(14, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 1, Name = "Friday", StartTime = new TimeSpan(15, 00, 00), EndTime = new TimeSpan(21, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Friday", StartTime = new TimeSpan(08, 00, 00), EndTime = new TimeSpan(12, 00, 00) });
            WorkingDayList.Add(new WorkingDay { EmployeeId = 2, Name = "Friday", StartTime = new TimeSpan(13, 00, 00), EndTime = new TimeSpan(19, 00, 00) });

        }

        public async Task CreateWorkingDayAsync(WorkingDay workingDay)
        {
            WorkingDayList.Add(workingDay);
        }

        // ??? Nu stiu daca o sa mai am nevoie de asta pt. ca deja fiecare employee are orele lui de lucru (EmployeeId).
        public async Task<WorkingDay> GetWorkingDayAsync(string nameOfDay)
        {
            return WorkingDayList.FirstOrDefault(obj => obj.Name == nameOfDay);
        }
        
        public async Task<IEnumerable<WorkingDay>> GetWorkingDayAsync(int employeeId, string nameOfDay)
        {
            return WorkingDayList
                .Where(obj => obj.EmployeeId == employeeId)
                .Where(obj => obj.Name == nameOfDay);
        }

        public async Task<IEnumerable<WorkingDay>> ReadWorkingDaysAsync()
        {
            return WorkingDayList;
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