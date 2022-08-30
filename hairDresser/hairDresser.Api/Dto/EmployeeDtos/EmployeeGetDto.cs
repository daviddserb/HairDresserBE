using hairDresser.Presentation.Dto.EmployeeHairServiceDtos;

namespace hairDresser.Presentation.Dto.EmployeeDtos
{
    public class EmployeeGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeeHairServiceDto> EmployeeHairServices { get; set; }
    }
}
