using hairDresser.Domain.Models;

namespace hairDresser.Presentation.Dto.WorkingIntervalDtos
{
    public class WorkingIntervalGetDto
    {
        public int Id { get; set; }
        public WorkingDay WorkingDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}