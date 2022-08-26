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
            await context.SaveChangesAsync();
        }
        public async Task<IQueryable<WorkingInterval>> ReadWorkingIntervalsAsync()
        {
            //Include spune ca faci JOIN intre WorkingIntervals si WorkingDay, care este posibil pt. ca ai ai un navigational property de Day in WorkingInterval si atunci poti face .WorkingDay pe variabila care salveaza acest return.
            return context.WorkingIntervals.Include(obj => obj.WorkingDay).Include(obj => obj.Employee);
        }

        // ? Nu stiu daca o sa ma folosesc de ea aceasta metoda, o sa vad...
        public async Task<WorkingInterval> GetWorkingIntervalAsync(int workingDayId)
        {
            throw new NotImplementedException();
            Console.WriteLine("WorkingIntervalRepository -> GetWorkingIntervalAsync(int workingDayId):");
            Console.WriteLine("nameDay= " + context.WorkingIntervals.First(day => day.WorkingDay.Id == workingDayId));
            return context.WorkingIntervals.First(day => day.WorkingDay.Id == workingDayId);
        }

        public async Task<IQueryable<WorkingInterval>> GetWorkingIntervalByEmployeeIdByWorkingDayIdAsync(int employeeId, int workingDayId)
        {
            Console.WriteLine("WorkingIntervalRepository -> GetWorkingIntervalByEmployeeIdByWorkingDayIdAsync(int employeeId, int workingDayId):");
            return context.WorkingIntervals
                .Where(wd => wd.Employee.Id == employeeId)
                .Where(wd => wd.WorkingDay.Id == workingDayId);
        }

        public async Task<WorkingInterval> UpdateWorkingIntervalAsync(WorkingInterval workingInterval)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteWorkingIntervalAsync(int workingIntervalId)
        {
            throw new NotImplementedException();
        }
    }
}
