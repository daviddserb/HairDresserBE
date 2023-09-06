using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.WorkingIntervalDtos
{
    public class WorkingIntervalPostDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "WorkingDayId must be between 1 and 5 inclusive.")]
        public int WorkingDayId { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }
    }
}