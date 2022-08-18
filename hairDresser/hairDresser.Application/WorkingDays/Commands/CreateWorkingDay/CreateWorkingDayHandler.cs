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
    public class CreateWorkingDayHandler : IRequestHandler<CreateWorkingDayQuery>
    {
        private IWorkingDayRepository _workingDayRepository;

        public CreateWorkingDayHandler(IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }

        public async Task<Unit> Handle(CreateWorkingDayQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle ->");

            var workingDay = new WorkingDay()
            {
                EmployeeId = request.EmployeeId,
                Name = request.NameOfDay,
                StartTime = TimeSpan.Parse(request.StartTime),
                EndTime = TimeSpan.Parse(request.EndTime),
            };

            await _workingDayRepository.CreateWorkingDayAsync(workingDay);

            return Unit.Value;
        }
    }
}
