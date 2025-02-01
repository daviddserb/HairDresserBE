using MediatR;

namespace hairDresser.Application.Users.Commands.DeleteEmployeeHairService
{
    public class DeleteHairServiceFromEmployeeCommand : IRequest
    {
        public int EmployeeHairServiceId { get; set; }
    }
}
