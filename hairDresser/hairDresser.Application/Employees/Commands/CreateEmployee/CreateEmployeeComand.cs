using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeComand : IRequest<Employee>
    {
        public string Name { get; set; }
        public List<int> HairServicesIds { get; set; }
    }
}
