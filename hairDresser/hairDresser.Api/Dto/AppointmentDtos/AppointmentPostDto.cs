using hairDresser.Presentation.CustomDataValidations;
using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;
using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.AppointmentDtos
{
    public class AppointmentPostDto
    {
        [Required(ErrorMessage = "Customer id required.")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Employee id required.")]
        public string EmployeeId { get; set; }

        [Required]
        public List<int> HairServicesIds { get; set; }

        [Required]
        [DateStart]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Price required.")]
        [Range(1, float.MaxValue, ErrorMessage = "The price must be between {1} and {2}.")]
        public float Price { get; set; }
    }
}
