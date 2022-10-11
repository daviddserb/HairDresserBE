using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);

            if (appointment == null) return null;

            var currentDate = DateTime.Now;
            currentDate.AddDays(1);
            if (appointment.StartDate > currentDate)
            {
                await _unitOfWork.AppointmentRepository.DeleteAppointmentAsync(request.AppointmentId);
                await _unitOfWork.SaveAsync();
            } else
            {
                throw new ClientException($"Can't cancel the appointment because it's too late!");
            }

            return appointment;
        }
    }
}
