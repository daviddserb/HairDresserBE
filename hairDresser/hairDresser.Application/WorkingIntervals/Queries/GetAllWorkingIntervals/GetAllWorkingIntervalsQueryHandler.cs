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
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkingIntervalsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<WorkingInterval>> Handle(GetAllWorkingIntervalsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WorkingIntervalRepository.ReadWorkingIntervalsAsync();
        }
    }
}

