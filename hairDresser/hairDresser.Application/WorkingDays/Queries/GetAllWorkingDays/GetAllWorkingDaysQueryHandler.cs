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
    public class GetAllWorkingDaysQueryHandler : IRequestHandler<GetAllWorkingDaysQuery, IQueryable<WorkingDay>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkingDaysQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IQueryable<WorkingDay>> Handle(GetAllWorkingDaysQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WorkingDayRepository.GetAllWorkingDaysAsync();
        }
    }
}
