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
        Task<WorkingInterval> GetWorkingIntervalByIdAsync(int workingIntervalId);
        Task<IQueryable<WorkingInterval>> GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(Guid employeeId, int workingDayId);
        Task<IQueryable<WorkingInterval>> GetAllWorkingIntervalsByEmployeeIdAsync(Guid employeeId);
        Task<WorkingInterval> UpdateWorkingIntervalAsync(WorkingInterval workingInterval);
        Task DeleteWorkingIntervalAsync(int workingIntervalId);
    }
}
