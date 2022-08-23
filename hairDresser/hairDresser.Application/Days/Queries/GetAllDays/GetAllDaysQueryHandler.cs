using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Days.Queries.GetAllDays
{
    public class GetAllDaysQueryHandler : IRequestHandler<GetAllDaysQuery, IEnumerable<Day>>
    {
        private readonly IDayRepository _dayRepository;

        public GetAllDaysQueryHandler(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }
        public async Task<IEnumerable<Day>> Handle(GetAllDaysQuery request, CancellationToken cancellationToken)
        {
            return await _dayRepository.ReadDaysAsync();
        }
    }
}
