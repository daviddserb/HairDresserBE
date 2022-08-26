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

        public async Task<IQueryable<WorkingDay>> ReadWorkingDaysAsync()
        {
            return context.WorkingDays;
        }

        // ? Nu stiu daca o sa ma folosesc de ea aceasta metoda, o sa vad...
        public async Task<WorkingDay> GetWorkingDayByIdAsync(int workingDayId)
        {
            Console.WriteLine("WorkingDayRepository -> GetWorkingDayAsync(int workingDayId):");
            Console.WriteLine($"nameOfWorkingDay= '{context.WorkingDays.First(obj => obj.Id == workingDayId).Name}'");
            return context.WorkingDays.First(obj => obj.Id == workingDayId);
        }

        public async Task<WorkingDay> GetWorkingDayByNameAsync(string workingDayName)
        {
            Console.WriteLine("WorkingDayRepository -> GetWorkingDayAsync(string workingDayName):");
            Console.WriteLine($"idOfWorkingDay= '{context.WorkingDays.First(obj => obj.Name == workingDayName).Id}'");
            return context.WorkingDays.First(obj => obj.Name == workingDayName);
        }
    }
}
