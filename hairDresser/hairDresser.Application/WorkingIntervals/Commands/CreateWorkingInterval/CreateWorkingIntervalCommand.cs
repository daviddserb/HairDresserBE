using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.WorkingIntervals.Commands.CreateWorkingInterval
{
    public class CreateWorkingIntervalCommand : IRequest<WorkingInterval>
    {
        public int WorkingDayId { get; set; }
        public string EmployeeId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}