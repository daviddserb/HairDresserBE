using AutoMapper;
using hairDresser.Application.Users.Commands.AddRoleToUser;
using hairDresser.Application.Users.Commands.LoginUser;
using hairDresser.Application.Users.Commands.RegisterUser;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.EmployeeDtos;
using hairDresser.Presentation.Dto.EmployeeHairServiceDtos;
using hairDresser.Presentation.Dto.UserDtos;

namespace hairDresser.Presentation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDto, RegisterUserCommand>();
            CreateMap<User, UserGetDto>();

            CreateMap<UserLoginDto, LoginUserCommand>();
            CreateMap<UserWithToken, UserGetDto>();

            CreateMap<UserRoleDto, AddRoleToUserCommand>();

            CreateMap<UserWithRole, UserGetDto>();

            CreateMap<User, EmployeeGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.EmployeeHairServices, opt => opt.MapFrom(src => src.EmployeeHairServices))
                .ForMember(dest => dest.EmployeeWorkingIntervals, opt => opt.MapFrom(src => src.EmployeeWorkingIntervals));
            CreateMap<EmployeeHairService, EmployeeHairServiceDto>();
            CreateMap<EmployeeFreeInterval, EmployeeFreeIntervalsGetDto>().ReverseMap();
        }
    }
}