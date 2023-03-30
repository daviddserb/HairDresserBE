using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.ReviewAppointment
{
    public class ReviewAppointmentCommandHandler : IRequestHandler<ReviewAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Appointment> Handle(ReviewAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment == null) throw new NotFoundException($"The appointment with the id '{request.AppointmentId}' does not exist!");
            if (appointment.ReviewId != null) throw new ClientException($"The appointment with the id '{request.AppointmentId}' already has a review!");
            // Make reviews available only for finished appointments
            var currentDate = DateTime.Now;
            if (appointment.EndDate >= currentDate) throw new ClientException($"Reviews are available only for finished appointments!");

            // Create a new review for the appointment
            var review = new Review
            {
                Rating = request.Rating,
                Comments = request.Comments
            };

            // Associate the review with the appointment -> will be saved the Id from the Review to the ReviewId from Appointment because of mapping from Review navigation property.
            appointment.Review = review;

            await _unitOfWork.AppointmentRepository.ReviewAppointmentAsync(review);
            await _unitOfWork.SaveAsync();

            return appointment;
        }
    }
}
