namespace hairDresser.Domain.Models
{
    public class AppointmentHairService
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public int HairServiceId { get; set; }
        public HairService HairService { get; set; }
    }
}
