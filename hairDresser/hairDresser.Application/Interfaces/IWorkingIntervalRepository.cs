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
        Task<IQueryable<WorkingInterval>> ReadWorkingIntervalsAsync();
        Task<WorkingInterval> GetWorkingIntervalAsync(int workingDayId);
        Task<IQueryable<WorkingInterval>> GetWorkingIntervalByEmployeeIdByWorkingDayIdAsync(int employeeId, int workingDayId);
        Task<WorkingInterval> UpdateWorkingIntervalAsync(WorkingInterval workingInterval);
        Task DeleteWorkingIntervalAsync(int workingIntervalId);
    }
}
