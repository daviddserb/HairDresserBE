using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;

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
            var employee = await _unitOfWork.UserRepository.GetUserByIdAsync(request.EmployeeId);
            if (employee == null) throw new NotFoundException($"The employee with the id '{request.EmployeeId}' does not exist!");

            return await _unitOfWork.HairServiceRepository.GetAcquiredHairServicesByEmployeeIdAsync(request.EmployeeId);
        }
    }
}
