using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;

namespace hairDresser.Presentation.Dto.AppointmentDtos
{
    public class AppointmentGetDto
    {
        // When somebody wants to get an appointment, this is the information that we want them to see.
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<AppointmentHairServiceDto>? AppointmentHairServices { get; set; }
    }
}
