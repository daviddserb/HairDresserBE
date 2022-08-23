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
    public class DayRepository : IDayRepository
    {
        private readonly DataContext context;

        public DayRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task CreateDayAsync(Day day)
        {
            await context.Days.AddAsync(day);
            await context.SaveChangesAsync();
        }

        public async Task<IQueryable<Day>> ReadDaysAsync()
        {
            return context.Days;
        }

        // ??? Nu stiu daca o sa ma folosesc de ea, voi vedea...
        public async Task<Day> GetDayAsync(int dayId)
        {
            Console.WriteLine("DayRepository -> GetDay(int dayId):");
            Console.WriteLine($"nameOfDay= '{context.Days.First(day => day.Id == dayId).Name}'");
            return context.Days.First(day => day.Id == dayId);
        }

        public async Task<Day> GetDayAsync(string dayName)
        {
            Console.WriteLine("DayRepository -> GetDay(string dayName):");
            Console.WriteLine($"idOfDay= '{context.Days.First(day => day.Name == dayName).Id}'");
            return context.Days.First(day => day.Name == dayName);
        }
    }
}
