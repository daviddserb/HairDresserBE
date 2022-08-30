using AutoMapper;
using hairDresser.Application.Employees.Commands.CreateEmployee;
using hairDresser.Application.Employees.Commands.DeleteEmployee;
using hairDresser.Application.Employees.Commands.UpdateEmployee;
using hairDresser.Application.Employees.Queries.GetAllEmployees;
using hairDresser.Application.Employees.Queries.GetEmployeeById;
using hairDresser.Application.Employees.Queries.GetEmployeesByServices;
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

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllEmployeesQuery();

            var allEmployees = await _mediator.Send(query);

            if (!allEmployees.Any()) return NotFound();

            var mappedEmployees = _mapper.Map<List<EmployeeGetDto>>(allEmployees);

            return Ok(mappedEmployees);
        }

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var query = new GetEmployeeByIdQuery { Id = employeeId };

            var employee = await _mediator.Send(query);

            if (employee == null) return NotFound();

            var mappedAppointment = _mapper.Map<EmployeeGetDto>(employee);

            return Ok(mappedAppointment);
        }

        [HttpGet]
        [Route("all/by-services")]
        public async Task<IActionResult> GetEmployeesByHairServices([FromQuery] List<int> hairServicesIds)
        {
            var query = new GetEmployeesByServicesQuery(hairServicesIds);

            var validEmployees = await _mediator.Send(query);

            if (!validEmployees.Any()) return NotFound();

            var mappedValidEmployees = _mapper.Map<List<EmployeeGetDto>>(validEmployees);

            return Ok(mappedValidEmployees);
        }

        //???
        //get intervale de la employee

        [HttpPut]
        [Route("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] EmployeePostPutDto editedEmployee)
        {
            var command = new UpdateEmployeeCommand
            {
                Id = employeeId,
                Name = editedEmployee.Name,
                HairServicesIds = editedEmployee.HairServicesIds
            };

            var result = await _mediator.Send(command);

            if (result == null) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<IActionResult> DeleteAppointment(int employeeId)
        {
            var command = new DeleteEmployeeCommand { Id = employeeId };

            var handlerResult = await _mediator.Send(command);

            if (handlerResult == null) return NotFound();

            return NoContent();
        }
    }
}
