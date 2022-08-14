using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IWorkingDayRepository
    {
        void CreateWorkingDay();
        IEnumerable<WorkingDay> GetAllWorkingDays();
        WorkingDay GetWorkingDay(string nameOfDay);
        void UpdateWorkingDay();
        void DeleteWorkingDay();
    }
}
