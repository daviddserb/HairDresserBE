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
            await context.SaveChangesAsync();
        }

        public async Task DeleteWorkingDayAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<WorkingDay> GetWorkingDayAsync(int dayId)
        {
            throw new NotImplementedException();
            Console.WriteLine("WorkingDayRepository -> GetWorkingDayAsync(int dayId):");
            Console.WriteLine("nameDay= " + context.WorkingDays.First(day => day.Day.Id == dayId));
            return context.WorkingDays.First(day => day.Day.Id == dayId);
        }

        public async Task<IEnumerable<WorkingDay>> GetWorkingDayAsync(int employeeId, int dayId)
        {
            Console.WriteLine("WorkingDayRepository -> GetWorkingDayAsync(int employeeId, int dayId):");
            return context.WorkingDays
                .Where(wd => wd.Employee.Id == employeeId)
                .Where(wd => wd.Day.Id == dayId);
        }

        public async Task<IEnumerable<WorkingDay>> ReadWorkingDaysAsync()
        {
            //Include spune ca faci JOIN intre WorkingDay si Day, care este posibil pt. ca ai cate un obiect (sau o proprietate) din cealalta clasa in ea, adica in WorkingDay ai o proprietate Day Day si la fel si invers.
            return context.WorkingDays.Include(x => x.Day);
        }

        public async Task UpdateWorkingDayAsync()
        {
            throw new NotImplementedException();
        }
    }
}
