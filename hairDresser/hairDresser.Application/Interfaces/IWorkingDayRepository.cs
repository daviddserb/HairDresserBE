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
        Task<WorkingDay> GetWorkingDayAsync(string nameOfDay);
        Task<IEnumerable<WorkingDay>> ReadWorkingDaysAsync();
        Task UpdateWorkingDayAsync();
        Task DeleteWorkingDayAsync();
    }
}
