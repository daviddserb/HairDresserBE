using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Days.Queries.GetAllDays
{
    public class GetAllDaysQuery : IRequest<IEnumerable<Day>>
    {
    }
}
