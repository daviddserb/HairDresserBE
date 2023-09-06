using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingIntervals.Queries.GetWorkingIntervalById
{
    public class GetWorkingIntervalByIdQueryHandler : IRequestHandler<GetWorkingIntervalByIdQuery, WorkingInterval>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWorkingIntervalByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkingInterval> Handle(GetWorkingIntervalByIdQuery request, CancellationToken cancellationToken)
        {
            var workingInterval = await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalByIdAsync(request.WorkingIntervalId);
            if (workingInterval == null) throw new NotFoundException($"There is no working interval registered with the id '{request.WorkingIntervalId}'!");
            return workingInterval;
        }
    }
}
