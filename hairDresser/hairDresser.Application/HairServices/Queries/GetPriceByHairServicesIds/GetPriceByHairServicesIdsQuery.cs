using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetPriceByHairServicesIds
{
    public class GetPriceByHairServicesIdsQuery : IRequest<float>
    {
        public List<int> HairServicesIds { get; set; }
    }
}
