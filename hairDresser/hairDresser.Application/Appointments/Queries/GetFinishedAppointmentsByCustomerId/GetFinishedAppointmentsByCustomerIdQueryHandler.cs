using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetFinishedAppointmentsByCustomerId
{
    public class GetFinishedAppointmentsByCustomerIdQueryHandler : IRequestHandler<GetFinishedAppointmentsByCustomerIdQuery, IQueryable<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFinishedAppointmentsByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Appointment>> Handle(GetFinishedAppointmentsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.UserRepository.GetUserByIdAsync(request.CustomerId);
            if (customer == null) throw new NotFoundException($"The customer with the id '{request.CustomerId}' does not exist!");

            var customerAppointmentsFinished = await _unitOfWork.AppointmentRepository.GetFinishedAppointmentsByCustomerIdAsync(request.CustomerId);
            if (!customerAppointmentsFinished.Any()) throw new NotFoundException($"The customer with the id '{request.CustomerId}' has no finished appointments!");
            return customerAppointmentsFinished;
        }
    }
}
