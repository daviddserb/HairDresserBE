using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public DeleteAppointmentCommandHandler(
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<Appointment> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var customerAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsByCustomerIdAsync(request.CustomerId);
            if (customerAppointments.All(appointment => appointment.Id != request.AppointmentId)) throw new ClientException($"The appointment with the id '{request.AppointmentId}' does not belong to the selected customer!");

            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment == null) throw new NotFoundException($"There is no appointment with the id '{request.AppointmentId}'!");
            if (appointment.isDeleted != null) throw new ClientException($"The appointment with the id '{request.AppointmentId}' already was canceled!");
            if (!(appointment.StartDate > DateTime.Now.AddDays(1))) throw new ClientException($"Appointments can be canceled only 24 hours before it starts!");

            await _unitOfWork.AppointmentRepository.DeleteAppointmentAsync(request.AppointmentId);
            await _unitOfWork.SaveAsync();

            await _cacheService.RemoveAsync($"FinishedAppointments:Employee:{appointment.EmployeeId}", cancellationToken);

            return appointment;
        }
    }
}