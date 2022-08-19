using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class WorkingDay
    {
        public int Id { get; set; }
        public int DayId { get; set; } //!!! Si in DB fac o Tabela Days cu Coloanele: Id si Names (Id = 1 => Names = Monday, Id = 2 => Names = Tuesday) si apoi Tabela WorkingDays are FK DayId spre Id din Tabela Days.
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
