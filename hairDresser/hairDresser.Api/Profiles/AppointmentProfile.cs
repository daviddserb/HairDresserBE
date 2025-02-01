using AutoMapper;
using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.AppointmentDtos;
using hairDresser.Presentation.Dto.AppointmentHairServiceDtos;

namespace hairDresser.Presentation.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            // CreateMap<Source, Destionation>();
            // When mapper.Map() is used (in Controller), it converts the object's data type to another one => we need to specify the data types here, based on the data type of each property and its name.
            // If the name is not the same or you want to change the name (or other reasons) you can use .ForMember to specify exactly how to do the changes, basically how to correlate each property.
            CreateMap<AppointmentPostDto, CreateAppointmentCommand>()
                .ReverseMap();

            // Here we map the Appointment to AppointmentGetDto and we want to extract only the CustomerName from the entire Customer, and same for the employee.
            CreateMap<Appointment, AppointmentGetDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.UserName))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.UserName))
                .ReverseMap();
            // After we map the Appointment to AppointmentGetDto, we need to map each collection (list) that has a different data type, so we need to make a separate Dto for each collection.
            CreateMap<AppointmentHairService, AppointmentHairServiceDto>()
                .ReverseMap();
        }
    }
}
