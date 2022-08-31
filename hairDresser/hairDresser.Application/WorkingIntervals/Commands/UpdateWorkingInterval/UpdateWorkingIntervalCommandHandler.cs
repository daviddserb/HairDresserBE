using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingIntervals.Commands.UpdateWorkingInterval
{
    public class UpdateWorkingIntervalCommandHandler : IRequestHandler<UpdateWorkingIntervalCommand, WorkingInterval>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkingIntervalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkingInterval> Handle(UpdateWorkingIntervalCommand request, CancellationToken cancellationToken)
        {
            var workingInterval = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalByIdAsync(request.WorkingIntervalId);

            if (workingInterval == null) return null;

            workingInterval.WorkingDayId = request.WorkingDayId;
            workingInterval.StartTime = TimeSpan.Parse(request.StartTime);
            workingInterval.EndTime = TimeSpan.Parse(request.EndTime);

            await _unitOfWork.WorkingIntervalRepository.UpdateWorkingIntervalAsync(workingInterval);
            await _unitOfWork.SaveAsync();

            return workingInterval;
        }
    }
}
