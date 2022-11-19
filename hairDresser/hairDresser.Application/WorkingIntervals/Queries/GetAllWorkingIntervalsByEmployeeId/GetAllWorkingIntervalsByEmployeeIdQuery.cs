using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeId
{
    public class GetAllWorkingIntervalsByEmployeeIdQuery : IRequest<IQueryable<WorkingInterval>>
    {
        public string EmployeeId { get; set; }
    }
}
