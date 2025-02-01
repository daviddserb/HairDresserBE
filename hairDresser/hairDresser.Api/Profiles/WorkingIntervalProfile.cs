using AutoMapper;
using hairDresser.Application.WorkingIntervals.Commands.CreateWorkingInterval;
using hairDresser.Application.WorkingIntervals.Commands.UpdateWorkingInterval;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.WorkingIntervalDtos;

namespace hairDresser.Presentation.Profiles
{
    public class WorkingIntervalProfile : Profile
    {
        public WorkingIntervalProfile()
        {
            CreateMap<WorkingIntervalPostDto, CreateWorkingIntervalCommand>().ReverseMap();

            CreateMap<WorkingInterval, WorkingIntervalGetDto>().ReverseMap();

            CreateMap<WorkingIntervalPutDto, UpdateWorkingIntervalCommand>().ReverseMap();
        }
    }
}