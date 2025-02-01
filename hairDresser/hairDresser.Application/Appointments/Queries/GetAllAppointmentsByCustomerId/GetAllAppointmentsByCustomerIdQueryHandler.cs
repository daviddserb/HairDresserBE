using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId
{
    public class GetAllAppointmentsByCustomerIdQueryHandler : IRequestHandler<GetAllAppointmentsByCustomerIdQuery, IQueryable<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAppointmentsByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Appointment>> Handle(GetAllAppointmentsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.UserRepository.GetUserByIdAsync(request.CustomerId);
            if (customer == null) throw new NotFoundException($"The customer with the id '{request.CustomerId}' does not exist!");

            var customerAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsByCustomerIdAsync(request.CustomerId);
            if (!customerAppointments.Any()) throw new NotFoundException($"The customer with the id '{request.CustomerId}' has no appointments!");
            return customerAppointments;
        }
    }
}
