using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Commands.DeleteWorkingInterval
{
    public class DeleteWorkingIntervalCommandHandler : IRequestHandler<DeleteWorkingIntervalCommand, WorkingInterval>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkingIntervalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkingInterval> Handle(DeleteWorkingIntervalCommand request, CancellationToken cancellationToken)
        {
            var workingInterval = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalByIdAsync(request.WorkingIntervalId);
            if (workingInterval == null) throw new NotFoundException($"There is no working interval registered with the id '{request.WorkingIntervalId}'!");

            await _unitOfWork.WorkingIntervalRepository.DeleteWorkingIntervalAsync(request.WorkingIntervalId);
            await _unitOfWork.SaveAsync();

            return workingInterval;
        }
    }
}
