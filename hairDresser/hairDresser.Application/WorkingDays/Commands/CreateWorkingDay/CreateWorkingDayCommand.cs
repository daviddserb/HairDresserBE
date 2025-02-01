using MediatR;

namespace hairDresser.Application.WorkingDays.Commands.CreateWorkingDay
{
    public class CreateWorkingDayCommand : IRequest
    {
        public string Name { get; set; }
    }
}
