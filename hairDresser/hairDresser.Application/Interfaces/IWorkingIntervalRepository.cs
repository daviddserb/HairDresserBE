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

        Task<WorkingInterval> GetWorkingIntervalByIdAsync(int workingIntervalId);

        Task<IQueryable<WorkingInterval>> GetAllWorkingIntervalsByEmployeeIdAsync(string employeeId);
        Task<IQueryable<WorkingInterval>> GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(string employeeId, int workingDayId);

        Task<WorkingInterval> UpdateWorkingIntervalAsync(WorkingInterval workingInterval);

        Task DeleteWorkingIntervalAsync(int workingIntervalId);
    }
}
