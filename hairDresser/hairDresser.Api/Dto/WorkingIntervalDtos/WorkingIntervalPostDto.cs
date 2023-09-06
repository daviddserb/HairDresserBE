using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.WorkingIntervalDtos
{
    public class WorkingIntervalPostDto
    {
        public int WorkingDayId { get; set; }
        public string EmployeeId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}