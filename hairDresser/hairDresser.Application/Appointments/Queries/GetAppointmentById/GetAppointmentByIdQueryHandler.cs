using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment == null) throw new NotFoundException($"There is no appointment registered with the id '{request.AppointmentId}'!");
            return appointment;
        }
    }
}
