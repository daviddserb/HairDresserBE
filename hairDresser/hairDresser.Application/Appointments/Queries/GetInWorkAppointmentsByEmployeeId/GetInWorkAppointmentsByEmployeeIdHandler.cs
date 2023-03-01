using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByEmployeeId
{
    public class GetInWorkAppointmentsByEmployeeIdHandler : IRequestHandler<GetInWorkAppointmentsByEmployeeId, IQueryable<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInWorkAppointmentsByEmployeeIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Appointment>> Handle(GetInWorkAppointmentsByEmployeeId request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (employee == null) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' does not exist!");

            var employeeInWorkAppointments = await _unitOfWork.AppointmentRepository.GetInWorkAppointmentsByEmployeeIdAsync(request.EmployeeId);
            if (!employeeInWorkAppointments.Any()) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' has no in work appointments!");
            return employeeInWorkAppointments;
        }
    }
}
