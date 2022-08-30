using AutoMapper;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.CustomerDtos;

namespace hairDresser.Presentation.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerGetDto>().ReverseMap();
        }
    }
}
