using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeId
{
    public class GetAllWorkingIntervalsByEmployeeIdQueryHandler : IRequestHandler<GetAllWorkingIntervalsByEmployeeIdQuery, IQueryable<WorkingInterval>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkingIntervalsByEmployeeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<WorkingInterval>> Handle(GetAllWorkingIntervalsByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WorkingIntervalRepository.GetAllWorkingIntervalsByEmployeeIdAsync(request.EmployeeId);
        }
    }
}
