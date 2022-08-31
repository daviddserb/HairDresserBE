using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeIdByDate
{
    public class GetAllWorkingIntervalsByEmployeeIdByDateQueryHandler : IRequestHandler<GetAllWorkingIntervalsByEmployeeIdByDateQuery, IQueryable<WorkingInterval>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkingIntervalsByEmployeeIdByDateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<WorkingInterval>> Handle(GetAllWorkingIntervalsByEmployeeIdByDateQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WorkingIntervalRepository.GetWorkingIntervalsByEmployeeIdByWorkingDayIdAsync(request.EmployeeId, request.WorkingDayId);
        }
    }
}
