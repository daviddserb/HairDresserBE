using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<Appointment>
    {
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public List<int> HairServicesIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
