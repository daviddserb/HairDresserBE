using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingDays.Commands.CreateWorkingDay
{
    public class CreateWorkingDayCommandHandler : IRequestHandler<CreateWorkingDayCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkingDayCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateWorkingDayCommand request, CancellationToken cancellationToken)
        {
            var workingDay = new WorkingDay
            {
                Name = request.Name
            };
            await _unitOfWork.WorkingDayRepository.CreateWorkingDayAsync(workingDay);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
