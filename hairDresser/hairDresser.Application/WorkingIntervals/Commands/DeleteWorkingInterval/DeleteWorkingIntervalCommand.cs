using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Commands.DeleteWorkingInterval
{
    public class DeleteWorkingIntervalCommand : IRequest<WorkingInterval>
    {
        public int WorkingIntervalId { get; set; }
    }
}
