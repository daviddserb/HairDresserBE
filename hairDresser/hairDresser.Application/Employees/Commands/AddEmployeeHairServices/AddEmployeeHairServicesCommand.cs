using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.AddEmployeeHairServices
{
    public class AddEmployeeHairServicesCommand : IRequest<Employee>
    {
        public int EmployeeId { get; set; }
        public List<int> HairServicesIds { get; set; }
    }
}
