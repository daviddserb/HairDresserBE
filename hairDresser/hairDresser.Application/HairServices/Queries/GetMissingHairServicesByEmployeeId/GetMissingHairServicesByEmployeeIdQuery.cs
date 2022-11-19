using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetMissingHairServicesByEmployeeId
{
    public class GetMissingHairServicesByEmployeeIdQuery : IRequest<List<HairService>>
    {
        public string EmployeeId { get; set; }
    }
}
