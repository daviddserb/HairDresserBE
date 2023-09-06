using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.WorkingIntervalDtos
{
    public class WorkingIntervalPutDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "WorkingDayId must be between 1 and 5 inclusive.")]
        public int WorkingDayId { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
