using hairDresser.Domain.Constants;

namespace hairDresser.Application.Interfaces
{
    public interface IWorkingDayRepository
    {
        Task<IEnumerable<WorkingDay>> GetAllWorkingDays();

        Task<WorkingDay?> GetWorkingDayById(int dayId);

        Task<WorkingDay?> GetWorkingDayByName(string dayName);
    }
}