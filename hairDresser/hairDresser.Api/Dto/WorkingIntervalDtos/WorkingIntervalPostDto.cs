using System.ComponentModel;
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

        /// <summary>
        /// Use string instead of TimeSpan because it's a struct that represents a duration of time as a number of ticks.
        /// </summary>
        [Required]
        [DefaultValue("08:30:00")]
        public string StartTime { get; set; }

        [Required]
        [DefaultValue("13:00:00")]
        public string EndTime { get; set; }
    }
}