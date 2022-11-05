using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeIdByDate
{
    public class GetAllWorkingIntervalsByEmployeeIdByDateQuery : IRequest<IQueryable<WorkingInterval>>
    {
        public Guid EmployeeId { get; set; }
        public int WorkingDayId { get; set; }
    }
}
