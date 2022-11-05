using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.CreateAppointment
{
    // Ca sa folosim libraria de MediatR, trebuie sa implementam interfata IRequest, care poate primi un parametru sau nu, care reprezinta tipul de date returnat.
    public class CreateAppointmentCommand : IRequest<Appointment>
    {
        // Acestea sunt proprietatile fara de care nu pot sa fac un appointment, adica am nevoie de user input in toate si le voi putea folosi in Handler.
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public List<int> HairServicesIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
    }
}
