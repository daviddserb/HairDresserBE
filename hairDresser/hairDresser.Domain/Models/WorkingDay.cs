namespace hairDresser.Domain.Models
{
    public class WorkingDay
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<WorkingInterval> WorkingIntervals { get; set; }
    }
}
