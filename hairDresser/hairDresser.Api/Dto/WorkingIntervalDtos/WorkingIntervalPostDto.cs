namespace hairDresser.Presentation.Dto.WorkingIntervalDtos
{
    public class WorkingIntervalPostDto
    {
        public int WorkingDayId { get; set; }
        public int EmployeeId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
