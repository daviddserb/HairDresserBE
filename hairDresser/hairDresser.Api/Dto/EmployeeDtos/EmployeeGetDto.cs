using hairDresser.Presentation.Dto.AppointmentDtos;
using hairDresser.Presentation.Dto.EmployeeHairServiceDtos;
using hairDresser.Presentation.Dto.WorkingIntervalDtos;

namespace hairDresser.Presentation.Dto.EmployeeDtos
{
    public class EmployeeGetDto
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public ICollection<EmployeeHairServiceDto> EmployeeHairServices { get; set; }

        public ICollection<WorkingIntervalGetDto> EmployeeWorkingIntervals { get; set; }
    }
}
