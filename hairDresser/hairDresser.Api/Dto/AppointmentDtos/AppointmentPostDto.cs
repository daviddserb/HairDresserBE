using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;
using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.AppointmentDtos
{
    public class AppointmentPostDto
    {
        [Required]
        public int? CustomerId { get; set; }

        [Required]
        public int? EmployeeId { get; set; }

        [Required]
        public List<int>? HairServicesIds { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
    }
}
