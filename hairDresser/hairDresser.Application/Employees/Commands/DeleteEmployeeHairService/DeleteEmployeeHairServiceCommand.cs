using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.DeleteEmployeeHairService
{
    public class DeleteEmployeeHairServiceCommand : IRequest
    {
        public int EmployeeHairServiceId { get; set; }
    }
}
