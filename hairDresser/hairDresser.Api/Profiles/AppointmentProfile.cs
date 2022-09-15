using AutoMapper;
using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Commands.UpdateAppointment;
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
            // Source -> Destionation

            CreateMap<AppointmentPostDto, CreateAppointmentCommand>().ReverseMap();

            // Cand vrem sa facem Get la Appointment, noi il extragem din DB si este de tipul Appointment, si aici il facem de tipul AppointmentGetDto.
            // Apoi cand facem comparatia intre proprietatile (tipul de date si nume) din Appointment si AppointmentGetDto, difera tipul colectiei => trebuie sa o mapam si pe ea.
            CreateMap<Appointment, AppointmentGetDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));
            // Aici mapez si tipurile de la colectie.
            CreateMap<AppointmentHairService, AppointmentHairServiceDto>().ReverseMap();

            CreateMap<AppointmentPutDto, UpdateAppointmentCommand>().ReverseMap();
        }
    }
}
