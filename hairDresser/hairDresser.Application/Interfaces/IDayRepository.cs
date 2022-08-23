using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IDayRepository
    {
        Task CreateDayAsync(Day day);
        Task<IQueryable<Day>> ReadDaysAsync();
        Task<Day> GetDayAsync(int dayId);
        Task<Day> GetDayAsync(string dayName);
    }
}
