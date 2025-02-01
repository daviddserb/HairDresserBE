using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Queries.GetWorkingIntervalById
{
    public class GetWorkingIntervalByIdQuery : IRequest<WorkingInterval>
    {
        public int WorkingIntervalId { get; set; }
    }
}
