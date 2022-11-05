using hairDresser.Application.CustomExceptions;
using hairDresser.Application.HairServices.Queries.GetAllHairServicesByEmployeeId;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetMissingHairServicesByEmployeeId
{
    public class GetMissingHairServicesByEmployeeIdQueryHandler : IRequestHandler<GetMissingHairServicesByEmployeeIdQuery, List<HairService>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMissingHairServicesByEmployeeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HairService>> Handle(GetMissingHairServicesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (employee == null) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' does not exist!");

            return await _unitOfWork.HairServiceRepository.GetMissingHairServicesByEmployeeId(request.EmployeeId);
        }
    }
}
