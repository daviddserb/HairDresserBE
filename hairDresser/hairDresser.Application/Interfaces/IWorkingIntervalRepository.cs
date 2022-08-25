using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IWorkingIntervalRepository
    {
        Task CreateWorkingIntervalAsync(WorkingInterval workingInterval);
        Task<IEnumerable<WorkingInterval>> ReadWorkingIntervalsAsync();
        Task<WorkingInterval> GetWorkingIntervalAsync(int dayId);
        Task<IEnumerable<WorkingInterval>> GetWorkingIntervalByEmployeeIdByWorkingDayIdAsync(int employeeId, int workingDayId);
        Task UpdateWorkingIntervalAsync();
        Task DeleteWorkingIntervalAsync();
    }
}
