using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;

namespace hairDresser.Infrastructure.Repositories
{
    public class WorkingDayRepository : IWorkingDayRepository
    {
        public Task<IEnumerable<WorkingDay>> GetAllWorkingDays()
        {
            var workingDays = Enum.GetValues(typeof(WorkingDay)).Cast<WorkingDay>();
            return Task.FromResult(workingDays);
        }

        public Task<WorkingDay?> GetWorkingDayById(int workingDayId)
        {
            if (Enum.IsDefined(typeof(WorkingDay), workingDayId))
            {
                return Task.FromResult<WorkingDay?>((WorkingDay)workingDayId);
            }
            return Task.FromResult<WorkingDay?>(null);
        }

        public Task<WorkingDay?> GetWorkingDayByName(string workingDayName)
        {
            if (Enum.TryParse<WorkingDay>(workingDayName, true, out var result))
            {
                return Task.FromResult<WorkingDay?>(result);
            }
            return Task.FromResult<WorkingDay?>(null);
        }
    }
}