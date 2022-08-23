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
            await context.SaveChangesAsync();
        }

        public async Task DeleteWorkingDayAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<WorkingDay> GetWorkingDayAsync(int dayId)
        {
            Console.WriteLine("WorkingDayRepository -> GetWorkingDayAsync(int dayId):");
            Console.WriteLine("nameDay= " + context.WorkingDays.First(day => day.DayId == dayId));
            return context.WorkingDays.First(day => day.DayId == dayId);
        }

        public async Task<IEnumerable<WorkingDay>> GetWorkingDayAsync(int employeeId, int dayId)
        {
            Console.WriteLine("WorkingDayRepository -> GetWorkingDayAsync(int employeeId, int dayId):");
            return context.WorkingDays
                .Where(wd => wd.EmployeeId == employeeId)
                .Where(wd => wd.DayId == dayId);
            //.Where(obj => obj.Name == nameOfDay); asta era inainte de al 2-lea Where
        }

        public async Task<IEnumerable<WorkingDay>> ReadWorkingDaysAsync()
        {
            return context.WorkingDays;
        }

        public async Task UpdateWorkingDayAsync()
        {
            throw new NotImplementedException();
        }
    }
}
