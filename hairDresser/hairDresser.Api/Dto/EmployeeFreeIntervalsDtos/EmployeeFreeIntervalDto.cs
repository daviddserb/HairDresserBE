namespace hairDresser.Presentation.Dto.EmployeeFreeIntervalsDtos
{
    public class EmployeeFreeIntervalDto
    {
        public string EmployeeId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        // Date = number that represents the day of the mounth (1, 2, 3, ..., 31).
        public int Date { get; set; }

        public int DurationInMinutes { get; set; }

        public string CustomerId { get; set; }
    }
}
