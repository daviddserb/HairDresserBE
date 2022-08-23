using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingDays.Queries.GetAllWorkingDays
{
    public class GetAllWorkingDaysQueryHandler : IRequestHandler<GetAllWorkingDaysQuery, IEnumerable<WorkingDay>>
    {
        private readonly IWorkingDayRepository _workingDayRepository;
        public GetAllWorkingDaysQueryHandler(IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }

        public async Task<IEnumerable<WorkingDay>> Handle(GetAllWorkingDaysQuery request, CancellationToken cancellationToken)
        {
            var allWorkingDays = await _workingDayRepository.ReadWorkingDaysAsync();
            return allWorkingDays;
        }
    }
}

