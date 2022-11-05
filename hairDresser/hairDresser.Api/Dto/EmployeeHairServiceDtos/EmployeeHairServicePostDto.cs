namespace hairDresser.Presentation.Dto.EmployeeHairServiceDtos
{
    public class EmployeeHairServicePostDto
    {
        public Guid EmployeeId { get; set; }
        public List<int> HairServicesIds { get; set; }
    }
}
