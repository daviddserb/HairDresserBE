using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetHairServicesByIds
{
    public class GetHairServicesByIdsQuery : IRequest<IQueryable<HairService>>
    {
        public List<int> HairServicesIds { get; set; }
    }
}
