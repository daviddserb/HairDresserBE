using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingDays.Queries.GetAllWorkingDays
{
    public class GetAllWorkingDaysQuery : IRequest<IQueryable<WorkingDay>>
    {
    }
}
