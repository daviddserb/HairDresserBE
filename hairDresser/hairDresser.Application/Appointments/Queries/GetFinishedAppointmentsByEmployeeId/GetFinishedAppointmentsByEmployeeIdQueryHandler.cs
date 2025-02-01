using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetFinishedAppointmentsByEmployeeId
{
    public class GetFinishedAppointmentsByEmployeeIdQueryHandler : IRequestHandler<GetFinishedAppointmentsByEmployeeIdQuery, IQueryable<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFinishedAppointmentsByEmployeeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Appointment>> Handle(GetFinishedAppointmentsByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (employee == null) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' does not exist!");

            var employeeFinishedAppointments = await _unitOfWork.AppointmentRepository.GetFinishedAppointmentsByEmployeeIdAsync(request.EmployeeId);
            if (!employeeFinishedAppointments.Any()) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' has no finished appointments!");
            return employeeFinishedAppointments;
        }
    }
}
