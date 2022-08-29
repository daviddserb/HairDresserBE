using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.AppointmentHairServiceDtos
{
    public class AppointmentHairServiceDto
    {
        // Asta este mai mult de verificare, sa vezi daca coreleaza (sunt la fel) id appointment.
        //public int AppointmentId { get; set; }
        // o sa trb. sa mai pun entitati ca sa afisez numele lui hairserviceid
        public int HairServiceId { get; set; }
    }
}
