using hairDresser.Application.Interfaces;
using hairDresser.Domain;
using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IQueryable<WorkingDay>> ReadWorkingDaysAsync()
        {
            return context.WorkingDays;
        }

        // ??? I still don't use this method but maybe in future.
        public async Task<WorkingDay> GetWorkingDayByIdAsync(int workingDayId)
        {
            return context.WorkingDays.First(obj => obj.Id == workingDayId);
        }

        public async Task<WorkingDay> GetWorkingDayByNameAsync(string workingDayName)
        {
            return context.WorkingDays.First(obj => obj.Name == workingDayName);
        }
    }
}
