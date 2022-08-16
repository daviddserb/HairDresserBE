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
        public List<WorkingDay> WorkingDayList = new List<WorkingDay>();

        public WorkingDayRepository()
        {
            WorkingDayList.Add(new WorkingDay { Name = "Monday", StartTime = new TimeSpan(08, 00, 00), EndTime = new TimeSpan(16, 00, 00) });
            WorkingDayList.Add(new WorkingDay { Name = "Tuesday", StartTime = new TimeSpan(08, 30, 00), EndTime = new TimeSpan(16, 30, 00) });
            WorkingDayList.Add(new WorkingDay { Name = "Wednesday", StartTime = new TimeSpan(09, 00, 00), EndTime = new TimeSpan(17, 00, 00) });
            WorkingDayList.Add(new WorkingDay { Name = "Thursday", StartTime = new TimeSpan(09, 30, 00), EndTime = new TimeSpan(17, 30, 00) });
            WorkingDayList.Add(new WorkingDay { Name = "Friday", StartTime = new TimeSpan(10, 00, 00), EndTime = new TimeSpan(18, 00, 00) });
        }

        public void CreateWorkingDay()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkingDay> GetAllWorkingDays()
        {
            return WorkingDayList;
        }

        public WorkingDay GetWorkingDay(string nameOfDay)
        {
            return WorkingDayList.FirstOrDefault(obj => obj.Name == nameOfDay);
        }

        public void UpdateWorkingDay()
        {
            throw new NotImplementedException();
        }
        public void DeleteWorkingDay()
        {
            throw new NotImplementedException();
        }
    }
}
