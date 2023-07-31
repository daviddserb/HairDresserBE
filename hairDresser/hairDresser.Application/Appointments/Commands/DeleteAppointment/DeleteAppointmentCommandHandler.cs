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
            var customerAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsByCustomerIdAsync(request.CustomerId);
            if (customerAppointments.All(appointment => appointment.Id != request.AppointmentId)) throw new ClientException($"The appointment with the id '{request.AppointmentId}' does not belong to the selected customer!");

            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment == null) throw new NotFoundException($"There is no appointment with the id '{request.AppointmentId}'!");
            if (appointment.isDeleted != null) throw new ClientException($"The appointment with the id '{request.AppointmentId}' already was canceled!");
            // Allow appointments to be canceled only if the start date of the appointment is at least 1 day ahead of the current time when trying to cancel it.
            if (!(appointment.StartDate > DateTime.Now.AddDays(1))) throw new ClientException($"Appointments can be canceled only 24 hours before it starts!");

            await _unitOfWork.AppointmentRepository.DeleteAppointmentAsync(request.AppointmentId);
            await _unitOfWork.SaveAsync();

            return appointment;
        }
    }
}
