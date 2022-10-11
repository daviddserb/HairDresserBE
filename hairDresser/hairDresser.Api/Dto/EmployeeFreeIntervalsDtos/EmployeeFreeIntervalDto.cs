namespace hairDresser.Presentation.Dto.EmployeeFreeIntervalsDtos
{
    public class EmployeeFreeIntervalDto
    {
        public int EmployeeId { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        // Date = nr. zilei din luna (1, 15, ..., 29, ..., 31).
        public int Date { get; set; }

        public int DurationInMinutes { get; set; }

        public int CustomerId { get; set; }
    }
}
