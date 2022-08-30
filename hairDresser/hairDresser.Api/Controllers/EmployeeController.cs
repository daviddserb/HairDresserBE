using AutoMapper;
using hairDresser.Application.Employees.Commands.CreateEmployee;
using hairDresser.Presentation.Dto.EmployeeDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public EmployeeController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeePostPutDto employee)
        {
            var command = _mapper.Map<CreateEmployeeComand>(employee);

            var employeeId = await _mediator.Send(command);

            return Created("", employeeId);
        }
    }
}
