using hairDresser.Application.CustomExceptions;
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
    public class GetMissingHairServicesByEmployeeIdQueryHandler : IRequestHandler<GetMissingHairServicesByEmployeeIdQuery, IQueryable<HairService>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMissingHairServicesByEmployeeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<HairService>> Handle(GetMissingHairServicesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (employee == null) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' does not exist!");

            return await _unitOfWork.HairServiceRepository.GetMissingHairServicesByEmployeeIdAsync(request.EmployeeId);
        }
    }
}
