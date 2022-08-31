using AutoMapper;
using hairDresser.Application.Customers.Commands.CreateCustomer;
using hairDresser.Application.Customers.Commands.UpdateCustomer;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.CustomerDtos;

namespace hairDresser.Presentation.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerPostPutDto, CreateCustomerCommand>().ReverseMap();

            CreateMap<Customer, CustomerGetDto>().ReverseMap();

            CreateMap<CustomerPostPutDto, UpdateCustomerCommand>().ReverseMap();

        }
    }
}
