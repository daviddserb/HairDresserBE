using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _unitOfWork.AppointmentRepository.GetAllAppointmentsByCustomerIdAsync(request.CustomerId);
        }
    }
}
