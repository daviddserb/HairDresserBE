using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetHairServiceById
{
    public class GetHairServiceByIdQueryHandler : IRequestHandler<GetHairServiceByIdQuery, HairService>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHairServiceByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HairService> Handle(GetHairServiceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.HairServiceRepository.GetHairServiceByIdAsync(request.Id);
        }
    }
}
