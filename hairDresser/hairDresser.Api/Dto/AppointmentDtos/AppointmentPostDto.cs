using hairDresser.Presentation.CustomDataValidations;
using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;
using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.AppointmentDtos
{
    public class AppointmentPostDto
    {
        [Required(ErrorMessage = "Customer id required.")]
        [Range(1, int.MaxValue)]
        public string? CustomerId { get; set; }

        [Required(ErrorMessage = "Employee id required.")]
        [Range(1, int.MaxValue)]
        public string? EmployeeId { get; set; }

        [Required]
        public List<int>? HairServicesIds { get; set; }

        [Required]
        [DateStart]
        public DateTime? StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        public DateTime? EndDate { get; set; }

        [Required]
        public float Price { get; set; }
    }
}
