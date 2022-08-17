using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Command.CreateEmployee
{
    public class CreateEmployeeComand : IRequest
    {
        public string Name { get; set; }
        public List<string> Specializations { get; set; }
    }
}
