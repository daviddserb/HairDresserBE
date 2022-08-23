using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Days.Commands.CreateDay
{
    public class CreateDayCommandHandler : IRequestHandler<CreateDayCommand>
    {
        private readonly IDayRepository _dayRepository;

        public CreateDayCommandHandler(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        public async Task<Unit> Handle(CreateDayCommand request, CancellationToken cancellationToken)
        {
            var day = new Day
            {
                Name = request.Name
            };
            await _dayRepository.CreateDayAsync(day);

            return Unit.Value;
        }
    }
}
