using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.WorkingIntervalDtos
{
    public class WorkingIntervalPostDto
    {
        public int? WorkingDayId { get; set; }
        public int? EmployeeId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string? StartTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string? EndTime { get; set; }
    }
}
