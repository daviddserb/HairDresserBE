using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByCustomerId
{
    public class GetInWorkAppointmentsByCustomerIdQueryHandler : IRequestHandler<GetInWorkAppointmentsByCustomerIdQuery, IQueryable<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInWorkAppointmentsByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Appointment>> Handle(GetInWorkAppointmentsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.UserRepository.GetUserByIdAsync(request.CustomerId);
            if (customer == null) throw new NotFoundException($"The customer with the id '{request.CustomerId}' does not exist!");

            var customerAppointmentsInWork = await _unitOfWork.AppointmentRepository.GetInWorkAppointmentsByCustomerIdAsync(request.CustomerId);
            if (!customerAppointmentsInWork.Any()) throw new NotFoundException($"The customer with the id '{request.CustomerId}' has no in work appointments!");

            return customerAppointmentsInWork;
        }
    }
}
