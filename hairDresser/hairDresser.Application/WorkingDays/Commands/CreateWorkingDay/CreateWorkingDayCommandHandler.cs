using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingDays.Commands.CreateWorkingDay
{
    public class CreateWorkingDayCommandHandler : IRequestHandler<CreateWorkingDayCommand>
    {
        private readonly IWorkingDayRepository _workingDayRepository;

        public CreateWorkingDayCommandHandler(IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }

        public async Task<Unit> Handle(CreateWorkingDayCommand request, CancellationToken cancellationToken)
        {
            var workingDay = new WorkingDay
            {
                Name = request.Name
            };
            await _workingDayRepository.CreateWorkingDayAsync(workingDay);
            return Unit.Value;
        }
    }
}
