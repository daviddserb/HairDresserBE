using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _unitOfWork.AppointmentRepository.GetInWorkAppointmentsByCustomerIdAsync(request.CustomerId);
        }
    }
}
