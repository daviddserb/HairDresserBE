using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.WorkingIntervalDtos
{
    public class WorkingIntervalPostDto
    {
        [Required]
        public int? WorkingDayId { get; set; }

        [Required]
        public int? EmployeeId { get; set; }

        [Required]
        public string? StartTime { get; set; }

        [Required]
        public string? EndTime { get; set; }
    }
}
