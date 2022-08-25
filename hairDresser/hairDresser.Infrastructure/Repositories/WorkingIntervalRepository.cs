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
        public async Task<IEnumerable<WorkingInterval>> ReadWorkingIntervalsAsync()
        {
            //Include spune ca faci JOIN intre WorkingIntervals si WorkingDay, care este posibil pt. ca ai ai un navigational property de Day in WorkingInterval si atunci poti face .WorkingDay pe variabila care salveaza acest return.
            return context.WorkingIntervals.Include(obj => obj.WorkingDay).Include(obj => obj.Employee);
        }

        // ? Nu stiu daca o sa ma folosesc de ea aceasta metoda, o sa vad...
        public async Task<WorkingInterval> GetWorkingIntervalAsync(int dayId)
        {
            throw new NotImplementedException();
            Console.WriteLine("WorkingIntervalRepository -> GetWorkingIntervalAsync(int dayId):");
            Console.WriteLine("nameDay= " + context.WorkingIntervals.First(day => day.WorkingDay.Id == dayId));
            return context.WorkingIntervals.First(day => day.WorkingDay.Id == dayId);
        }

        public async Task<IEnumerable<WorkingInterval>> GetWorkingIntervalByEmployeeIdByWorkingDayIdAsync(int employeeId, int workingDayId)
        {
            Console.WriteLine("WorkingIntervalRepository -> GetWorkingIntervalByEmployeeIdByDayIdAsync(int employeeId, int dayId):");
            return context.WorkingIntervals
                .Where(wd => wd.Employee.Id == employeeId)
                .Where(wd => wd.WorkingDay.Id == workingDayId);
        }

        public async Task UpdateWorkingIntervalAsync()
        {
            throw new NotImplementedException();
        }
        public async Task DeleteWorkingIntervalAsync()
        {
            throw new NotImplementedException();
        }
    }
}
