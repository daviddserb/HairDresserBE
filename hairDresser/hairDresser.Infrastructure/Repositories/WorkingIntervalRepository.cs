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
            //Include spune ca faci JOIN intre WorkingIntervals si WorkingDay, care este posibil pt. ca ai ai un navigational property de Day in WorkingInterval si atunci poti face .WorkingDay pe variabila care salveaza acest return.
            return context.WorkingIntervals
                .Include(obj => obj.WorkingDay)
                .Include(obj => obj.Employee);
        }

        public async Task<WorkingInterval> GetWorkingIntervalByIdAsync(int workingIntervalId)
        {
            return await context.WorkingIntervals
                .Include(obj => obj.WorkingDay)
                .Include(obj => obj.Employee)
                .FirstOrDefaultAsync(workingInterval => workingInterval.Id == workingIntervalId);
        }

        public async Task<IQueryable<WorkingInterval>> GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(int employeeId, int workingDayId)
        {
            return context.WorkingIntervals
                .Where(employee => employee.Employee.Id == employeeId)
                .Where(workingDay => workingDay.WorkingDay.Id == workingDayId)
                .Include(obj => obj.WorkingDay)
                .Include(obj => obj.Employee);
        }

        public async Task<IQueryable<WorkingInterval>> GetAllWorkingIntervalsByEmployeeIdAsync(int employeeId)
        {
            return context.WorkingIntervals
                .Where(employee => employee.Employee.Id == employeeId)
                .Include(obj => obj.WorkingDay)
                .Include(obj => obj.Employee); ;
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
