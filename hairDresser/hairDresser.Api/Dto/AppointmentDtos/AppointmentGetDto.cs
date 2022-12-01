using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;
using hairDresser.Presentation.Dto.CustomerDtos;
using hairDresser.Presentation.Dto.EmployeeDtos;

namespace hairDresser.Presentation.Dto.AppointmentDtos
{
    public class AppointmentGetDto
    {
        // All the properties that are selected to give on the command get of appointments.

        public int Id { get; set; }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public float Price { get; set; }

        public DateTime? isDeleted { get; set; }

        public ICollection<AppointmentHairServiceDto> AppointmentHairServices { get; set; }
    }
}
