using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingDays.Queries.GetAllWorkingDays
{
    public class GetAllWorkingDaysQuery : IRequest<IQueryable<WorkingDay>>
    {
    }
}
