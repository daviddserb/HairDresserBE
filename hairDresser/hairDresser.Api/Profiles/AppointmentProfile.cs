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

            // Cand vrem sa facem Get la Appointment, noi il extragem din DB si este de tipul Appointment, si aici il facem de tipul AppointmentGetDto.
            // Apoi cand facem comparatia intre proprietatile (tip si nume) din Appointment si AppointmentGetDto, difera tipul colectiei. Astfel trebuie sa o mapam.
            CreateMap<Appointment, AppointmentGetDto>().ReverseMap();
            // Aici o mapez.
            CreateMap<AppointmentHairService, AppointmentHairServiceDto>().ReverseMap();

            CreateMap<AppointmentPostPutDto, CreateAppointmentCommand>().ReverseMap();

            CreateMap<AppointmentPostPutDto, UpdateAppointmentCommand>().ReverseMap();

        }
    }
}
