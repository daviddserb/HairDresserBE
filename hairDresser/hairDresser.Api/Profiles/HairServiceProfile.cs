using AutoMapper;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.HairServiceDtos;

namespace hairDresser.Presentation.Profiles
{
    public class HairServiceProfile : Profile
    {
        public HairServiceProfile()
        {
            CreateMap<HairService, HairServiceGetDto>().ReverseMap().ReverseMap();
        }
    }
}
