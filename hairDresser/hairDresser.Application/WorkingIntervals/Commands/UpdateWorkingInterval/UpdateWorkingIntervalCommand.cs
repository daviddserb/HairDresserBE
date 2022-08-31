using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.WorkingIntervals.Commands.UpdateWorkingInterval
{
    public class UpdateWorkingIntervalCommand : IRequest<WorkingInterval>
    {
        public int WorkingIntervalId { get; set; }
        public int WorkingDayId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
