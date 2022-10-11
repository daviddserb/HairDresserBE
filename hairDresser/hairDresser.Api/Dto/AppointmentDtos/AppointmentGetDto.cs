using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;
using hairDresser.Presentation.Dto.CustomerDtos;
using hairDresser.Presentation.Dto.EmployeeDtos;

namespace hairDresser.Presentation.Dto.AppointmentDtos
{
    public class AppointmentGetDto
    {
        // When somebody wants to get an appointment, the properties defines the information that we select for the user to see.

        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public float Price { get; set; }

        public DateTime? isDeleted { get; set; }

        public ICollection<AppointmentHairServiceDto>? AppointmentHairServices { get; set; }
    }
}
