using AutoMapper;
using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.AppointmentDtos;
using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;
using hairDresser.Presentation.Dto.EmployeeDtos;

namespace hairDresser.Presentation.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            //CreateMap<Source, Destionation>();
            //When in the controller we use mapper.Map (which converts the object's data type to another one) we need to specify the data types here, based on the data type of each property and it's name.
            //If the name is not the same or you want to change the name (or other reasons) you can use .ForMember to tell exactly how to do the changes.
            CreateMap<AppointmentPostDto, CreateAppointmentCommand>().ReverseMap();

            //Here we map the Appointment to AppointmentGetDto and we want to extract only the CustomerName from the entire Customer (same for the employee).
            CreateMap<Appointment, AppointmentGetDto>()
                // BEFORE:
                //.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                //.ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));
                // AFTER:
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.UserName))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.UserName));
            //After we map the Appointment to AppointmentGetDto, we need to map each list/collection that has different data type, so we need to make a separate Dto for each collection.
            CreateMap<AppointmentHairService, AppointmentHairServiceDto>().ReverseMap();
        }
    }
}
