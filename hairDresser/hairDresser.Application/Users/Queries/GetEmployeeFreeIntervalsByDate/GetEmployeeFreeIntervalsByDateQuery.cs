using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Queries.GetEmployeeFreeIntervalsByDate
{
    public class GetEmployeeFreeIntervalsByDateQuery : IRequest<List<EmployeeFreeInterval>>
    {
        public string EmployeeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Date { get; set; }
        public int DurationInMinutes { get; set; }
        public string CustomerId { get; set; }
    }
}
