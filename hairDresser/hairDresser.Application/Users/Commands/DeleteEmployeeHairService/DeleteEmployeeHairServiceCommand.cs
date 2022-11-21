using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Users.Commands.DeleteEmployeeHairService
{
    public class DeleteEmployeeHairServiceCommand : IRequest
    {
        public int EmployeeHairServiceId { get; set; }
    }
}
