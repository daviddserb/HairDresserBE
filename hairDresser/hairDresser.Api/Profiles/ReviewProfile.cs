using AutoMapper;
using hairDresser.Application.Appointments.Commands.ReviewAppointment;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.ReviewDtos;

namespace hairDresser.Presentation.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewPostDto, ReviewAppointmentCommand>();

            CreateMap<Review, ReviewGetDto>();
        }
    }
}
