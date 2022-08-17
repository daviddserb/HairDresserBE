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
        Task CreateWorkingDayAsync();
        Task<IEnumerable<WorkingDay>> GetAllWorkingDaysAsync();
        Task<WorkingDay> GetWorkingDayAsync(string nameOfDay);
        Task UpdateWorkingDayAsync();
        Task DeleteWorkingDayAsync();
    }
}
