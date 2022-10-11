using AutoMapper;
using hairDresser.Application.HairServices.Commands.CreateHairService;
using hairDresser.Application.HairServices.Commands.UpdateHairService;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.HairServiceDtos;

namespace hairDresser.Presentation.Profiles
{
    public class HairServiceProfile : Profile
    {
        public HairServiceProfile()
        {
            CreateMap<HairServicePostPutDto, CreateHairServiceCommand>().ReverseMap();

            CreateMap<HairService, HairServiceGetDto>().ReverseMap();
            CreateMap<EmployeeHairService, HairServiceGetDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.HairService.Name))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.HairService.Duration))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.HairService.Price));

            CreateMap<HairServicePostPutDto, UpdateHairServiceCommand>().ReverseMap();
        }
    }
}
