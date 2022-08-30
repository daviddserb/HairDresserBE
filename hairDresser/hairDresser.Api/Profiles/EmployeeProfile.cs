using AutoMapper;
using hairDresser.Application.Employees.Commands.CreateEmployee;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.EmployeeDtos;

namespace hairDresser.Presentation.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeePostPutDto, CreateEmployeeComand>().ReverseMap();

            CreateMap<Employee, EmployeeGetDto>().ReverseMap();
        }
    }
}
