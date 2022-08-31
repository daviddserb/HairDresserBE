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

            CreateMap<HairServicePostPutDto, UpdateHairServiceCommand>().ReverseMap();
        }
    }
}
