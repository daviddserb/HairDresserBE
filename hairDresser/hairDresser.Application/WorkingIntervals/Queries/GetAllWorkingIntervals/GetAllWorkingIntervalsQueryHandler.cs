using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervals
{
    public class GetAllWorkingIntervalsQueryHandler : IRequestHandler<GetAllWorkingIntervalsQuery, IQueryable<WorkingInterval>>
    {
        private readonly IWorkingIntervalRepository _workingIntervalRepository;
        public GetAllWorkingIntervalsQueryHandler(IWorkingIntervalRepository workingIntervalRepository)
        {
            _workingIntervalRepository = workingIntervalRepository;
        }

        public async Task<IQueryable<WorkingInterval>> Handle(GetAllWorkingIntervalsQuery request, CancellationToken cancellationToken)
        {
            var allWorkingIntervals = await _workingIntervalRepository.ReadWorkingIntervalsAsync();
            return allWorkingIntervals;
        }
    }
}

