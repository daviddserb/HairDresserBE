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
        Task CreateWorkingDayAsync(WorkingDay workingDay);

        Task<IQueryable<WorkingDay>> GetAllWorkingDaysAsync();
        Task<WorkingDay> GetWorkingDayByIdAsync(int dayId);

        Task<WorkingDay> GetWorkingDayByNameAsync(string dayName);
    }
}
