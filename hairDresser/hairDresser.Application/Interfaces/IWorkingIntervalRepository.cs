using hairDresser.Domain.Models;

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
