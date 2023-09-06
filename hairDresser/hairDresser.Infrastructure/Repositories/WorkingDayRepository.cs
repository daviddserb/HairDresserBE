using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace hairDresser.Infrastructure.Repositories
{
    public class WorkingDayRepository : IWorkingDayRepository
    {
        private readonly DataContext context;

        public WorkingDayRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateWorkingDayAsync(WorkingDay workingDay)
        {
            await context.WorkingDays.AddAsync(workingDay);
        }

        public async Task<IQueryable<WorkingDay>> GetAllWorkingDaysAsync()
        {
            return context.WorkingDays;
        }

        public async Task<WorkingDay> GetWorkingDayByIdAsync(int workingDayId)
        {
            return await context.WorkingDays.FirstOrDefaultAsync(workingDay => workingDay.Id == workingDayId);
        }

        public async Task<WorkingDay> GetWorkingDayByNameAsync(string workingDayName)
        {
            return await context.WorkingDays.FirstOrDefaultAsync(workingDay => workingDay.Name == workingDayName);
        }
    }
}