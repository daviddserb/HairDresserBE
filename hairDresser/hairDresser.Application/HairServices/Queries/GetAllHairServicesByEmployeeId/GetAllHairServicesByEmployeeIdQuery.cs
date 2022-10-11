using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.HairServices.Queries.GetAllHairServicesByEmployeeId
{
    public class GetAllHairServicesByEmployeeIdQuery : IRequest<IQueryable<EmployeeHairService>>
    {
        public int EmployeeId { get; set; }
    }
}
