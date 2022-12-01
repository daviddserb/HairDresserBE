using hairDresser.Presentation.Dto.EmployeeHairServiceDtos;
using hairDresser.Presentation.Dto.WorkingIntervalDtos;

namespace hairDresser.Presentation.Dto.EmployeeDtos
{
    public class EmployeeGetDto
    {
        // BEFORE:
        //public int Id { get; set; }
        //public string Name { get; set; }
        // AFTER:
        public string Id { get; set; }
        public string Username { get; set; }

        public ICollection<EmployeeHairServiceDto> EmployeeHairServices { get; set; }

        //before:
        //public ICollection<EmployeeWorkingIntervalDto> EmployeeWorkingIntervals { get; set; }
        //after:
        public ICollection<WorkingIntervalGetDto> EmployeeWorkingIntervals { get; set; }
    }
}
