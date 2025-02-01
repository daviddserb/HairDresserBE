using hairDresser.Domain.Models;

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
