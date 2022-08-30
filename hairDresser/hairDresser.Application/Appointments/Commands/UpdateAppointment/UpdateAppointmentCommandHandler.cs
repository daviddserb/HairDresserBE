using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            
            if (appointment == null) return null;

            appointment.AppointmentHairServices = request.HairServicesId.Select(hsi => new AppointmentHairService()
            {
                HairServiceId = hsi
            }).ToList();
            appointment.EmployeeId = request.EmployeeId;
            appointment.StartDate = request.StartDate;
            appointment.EndDate = request.EndDate;

            await _unitOfWork.AppointmentRepository.UpdateAppointmentAsync(appointment);
            await _unitOfWork.SaveAsync();

            return appointment;
        }
    }
}
