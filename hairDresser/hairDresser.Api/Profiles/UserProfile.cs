using AutoMapper;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.EmployeeDtos;
using hairDresser.Presentation.Dto.UserDtos;

namespace hairDresser.Presentation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<ApplicationUser, UserGetDto>();

            CreateMap<ApplicationUser, EmployeeGetDto>();
        }
    }
}
