using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetAllHairServicesByEmployeeId
{
    public class GetAllHairServicesByEmployeeIdQueryHandler : IRequestHandler<GetAllHairServicesByEmployeeIdQuery, IQueryable<EmployeeHairService>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllHairServicesByEmployeeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<EmployeeHairService>> Handle(GetAllHairServicesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.HairServiceRepository.GetHairServicesByEmployeeId(request.EmployeeId);
        }
    }
}
