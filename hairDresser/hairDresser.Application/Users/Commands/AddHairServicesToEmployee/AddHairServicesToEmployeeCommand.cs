using hairDresser.Domain.Models;
using MediatR;

namespace hairDresser.Application.Users.Commands.AddHairServicesToEmployee
{
    public class AddHairServicesToEmployeeCommand : IRequest<User>
    {
        public string EmployeeId { get; set; }
        public List<int> HairServicesIds { get; set; }
    }
}
