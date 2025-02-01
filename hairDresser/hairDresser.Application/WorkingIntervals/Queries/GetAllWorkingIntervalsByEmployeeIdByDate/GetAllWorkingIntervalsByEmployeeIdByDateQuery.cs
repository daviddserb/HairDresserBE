using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeIdByDate
{
    public class GetAllWorkingIntervalsByEmployeeIdByDateQuery : IRequest<IQueryable<WorkingInterval>>
    {
        public string EmployeeId { get; set; }
        public int WorkingDayId { get; set; }
    }
}
