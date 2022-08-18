using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Commands.DeleteEmployeeById
{
    public class DeleteEmployeeByIdCommand : IRequest
    {
        public int Id { get; set; }
    }
}
