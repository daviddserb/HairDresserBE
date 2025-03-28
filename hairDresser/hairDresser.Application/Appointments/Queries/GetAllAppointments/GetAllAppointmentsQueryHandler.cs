﻿using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Appointments.Queries.GetAllAppointments
{
    public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, IQueryable<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAppointmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Appointment>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var allAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync(request.PageNumber, request.PageSize);
            if (!allAppointments.Any()) throw new NotFoundException("There are no appointments registered!");
            return allAppointments;
        }
    }
}