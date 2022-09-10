using hairDresser.Presentation.CustomDataValidations;
using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;
using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.AppointmentDtos
{
    public class AppointmentPostDto
    {
        [Required(ErrorMessage = "Customer id required.")]
        [Range(1, int.MaxValue)]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Employee id required.")]
        [Range(1, int.MaxValue)]
        public int? EmployeeId { get; set; }

        [Required]
        public List<int>? HairServicesIds { get; set; }

        [Required]
        //???
        //[DateNotInPast(StartDate)]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
    }
}
