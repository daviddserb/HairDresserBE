using AutoMapper;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.WorkingDayDtos;

namespace hairDresser.Presentation.Profiles
{
    public class WorkingDayProfile : Profile
    {
        public WorkingDayProfile()
        {
            CreateMap<WorkingDay, WorkingDayGetDto>().ReverseMap();

        }
    }
}
