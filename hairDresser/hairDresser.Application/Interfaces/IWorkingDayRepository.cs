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
        Task<WorkingDay> GetWorkingDayAsync(int dayId);
        Task<IEnumerable<WorkingDay>> GetWorkingDayAsync(int employeeId, string nameOfDay);
        Task<IEnumerable<WorkingDay>> ReadWorkingDaysAsync();
        Task UpdateWorkingDayAsync();
        Task DeleteWorkingDayAsync();
    }
}
