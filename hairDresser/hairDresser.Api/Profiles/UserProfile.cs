using AutoMapper;
using hairDresser.Application.Users.Commands.AddRoleToUser;
using hairDresser.Application.Users.Commands.LoginUser;
using hairDresser.Application.Users.Commands.RegisterUser;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.EmployeeDtos;
using hairDresser.Presentation.Dto.UserDtos;

namespace hairDresser.Presentation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap< UserRegisterDto, RegisterUserCommand>();
            CreateMap<User, UserGetDto>();

            CreateMap<UserLoginDto, LoginUserCommand>();
            CreateMap<UserToken, UserGetDto>();

            CreateMap<UserRoleDto, AddRoleToUserCommand>();

            CreateMap<User, EmployeeGetDto>();
            CreateMap<User, EmployeeGetDto>();
        }
    }
}
