using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.CreateAppointment
{
    // Ca sa folosim libraria de MediatR, trebuie sa implementam interfata IRequest, care poate primi un parametru sau nu, care reprezinta tipul de date pe care il returneaza dupa ce s-a efectuat operatia.
    public class CreateAppointmentCommand : IRequest<int>
    {
        // Acestea sunt proprietatile fara de care nu pot sa fac un appointment, adica am nevoie de user input in ele si le folosesc in Handler.
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public List<int> HairServicesId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
