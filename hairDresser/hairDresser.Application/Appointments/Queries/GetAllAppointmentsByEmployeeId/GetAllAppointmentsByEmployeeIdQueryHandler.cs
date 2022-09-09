using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointmentsByEmployeeId
{
    public class GetAllAppointmentsByEmployeeIdQueryHandler : IRequestHandler<GetAllAppointmentsByEmployeeIdQuery, IQueryable<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAppointmentsByEmployeeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Appointment>> Handle(GetAllAppointmentsByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AppointmentRepository.GetAllAppointmentsByEmployeeIdAsync(request.EmployeeId);
        }
    }
}
