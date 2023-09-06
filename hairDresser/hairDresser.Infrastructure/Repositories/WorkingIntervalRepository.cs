using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace hairDresser.Infrastructure.Repositories
{
    public class WorkingIntervalRepository : IWorkingIntervalRepository
    {
        private readonly DataContext context;

        public WorkingIntervalRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateWorkingIntervalAsync(WorkingInterval workingInterval)
        {
            await context.WorkingIntervals.AddAsync(workingInterval);
        }

        public async Task<WorkingInterval> GetWorkingIntervalByIdAsync(int workingIntervalId)
        {
            var workingInterval = await context.WorkingIntervals
                .Include(obj => obj.WorkingDay)
                .Include(obj => obj.Employee)
                .FirstOrDefaultAsync(workingInterval => workingInterval.Id == workingIntervalId);
            return workingInterval;
        }

        public async Task<IQueryable<WorkingInterval>> GetAllWorkingIntervalsByEmployeeIdAsync(string employeeId)
        {
            var employeeWorkingIntervals = context.WorkingIntervals
                .Where(employee => employee.Employee.Id == employeeId)
                .Include(workingDay => workingDay.WorkingDay)
                .Include(employee => employee.Employee)
                .OrderBy(workingDay => workingDay.WorkingDayId)
                .ThenBy(startTime => startTime.StartTime);
            return employeeWorkingIntervals;
        }

        /// <summary>
        /// After modifying the method's body (the query), ensure the Integration Tests are working correctly. It's essential because translating the query into SQLite can potentially lead to issues.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="workingDayId"></param>
        /// <returns></returns>
        public async Task<IQueryable<WorkingInterval>> GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(string employeeId, int workingDayId)
        {
            var employeeWorkingIntervalsInSelectedWorkingDay = context.WorkingIntervals
                .Where(employee => employee.Employee.Id == employeeId)
                .Where(workingDay => workingDay.WorkingDay.Id == workingDayId)
                .Include(obj => obj.WorkingDay)
                .Include(obj => obj.Employee);
            return employeeWorkingIntervalsInSelectedWorkingDay;
        }

        public async Task<WorkingInterval> UpdateWorkingIntervalAsync(WorkingInterval workingInterval)
        {
            context.WorkingIntervals.Update(workingInterval);
            return workingInterval;
        }

        public async Task DeleteWorkingIntervalAsync(int workingIntervalId)
        {
            var workingInterval = await context.WorkingIntervals.FirstOrDefaultAsync(workingInterval => workingInterval.Id == workingIntervalId);

            context.WorkingIntervals.Remove(workingInterval);
        }
    }
}
