using hairDresser.Application.Interfaces;
using hairDresser.Domain;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IQueryable<WorkingInterval>> ReadWorkingIntervalsAsync()
        {
            //Include = Join.
            return context.WorkingIntervals
                .Include(workingDay => workingDay.WorkingDay)
                .Include(employee => employee.Employee)
                .OrderBy(workingDay => workingDay.WorkingDayId)
                .ThenBy(startTime => startTime.StartTime);
        }

        public async Task<WorkingInterval> GetWorkingIntervalByIdAsync(int workingIntervalId)
        {
            var workingInterval = await context.WorkingIntervals
                .Include(obj => obj.WorkingDay)
                .Include(obj => obj.Employee)
                .FirstOrDefaultAsync(workingInterval => workingInterval.Id == workingIntervalId);

            return workingInterval;
        }

        public async Task<IQueryable<WorkingInterval>> GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(string employeeId, int workingDayId)
        {
            return context.WorkingIntervals
                .Where(employee => employee.Employee.Id.Equals(employeeId))
                .Where(workingDay => workingDay.WorkingDay.Id == workingDayId)
                .Include(obj => obj.WorkingDay)
                .Include(obj => obj.Employee);
        }

        public async Task<IQueryable<WorkingInterval>> GetAllWorkingIntervalsByEmployeeIdAsync(string employeeId)
        {
            var employeeWorkingIntervals = context.WorkingIntervals
                .Where(employee => employee.Employee.Id.Equals(employeeId))
                .Include(workingDay => workingDay.WorkingDay)
                .Include(employee => employee.Employee)
                .OrderBy(workingDay => workingDay.WorkingDayId)
                .ThenBy(startTime => startTime.StartTime);
            return employeeWorkingIntervals;
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
