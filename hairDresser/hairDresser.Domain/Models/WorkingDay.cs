using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class WorkingDay
    {
        public int Id { get; set; }

        // ??? Ii ok pus asta aici? Intreb pt. ca la Migrari, aveam doar 2 metode complet goale. Trebuie sa pun numele clasei "Day" sau numele tabelului "Days"? Am incercat ambele si migrarile au avut la fel 2 metode goale.
        // Nu cred ca merge pt. ca pot sa salvez si in alte zile, de ex. in Day nu exista Weekend, adica ii maxim pana la id-ul 5, dar eu pot sa salvez pe orice Id, adica si 6.
        [ForeignKey("Day")]
        public int DayId { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
