namespace hairDresser.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Comments { get; set; }

        public Appointment Appointment { get; set; } // navigation property for the Appointment
    }
}
