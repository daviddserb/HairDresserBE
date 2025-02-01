using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeId
{
    public class GetAllWorkingIntervalsByEmployeeIdQuery : IRequest<IQueryable<WorkingInterval>>
    {
        public string EmployeeId { get; set; }
    }
}
