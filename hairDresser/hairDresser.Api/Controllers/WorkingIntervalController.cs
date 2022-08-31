using AutoMapper;
using hairDresser.Application.WorkingIntervals.Commands.CreateWorkingInterval;
using hairDresser.Application.WorkingIntervals.Commands.DeleteWorkingInterval;
using hairDresser.Application.WorkingIntervals.Commands.UpdateWorkingInterval;
using hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervals;
using hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeId;
using hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeIdByDate;
using hairDresser.Presentation.Dto.WorkingIntervalDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkingIntervalController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public WorkingIntervalController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkingIntervalAsync([FromBody] WorkingIntervalPostDto workingInterval)
        {
            var command = _mapper.Map<CreateWorkingIntervalCommand>(workingInterval);

            var workingIntervalId = await _mediator.Send(command);

            if (workingIntervalId == -1) return BadRequest(); //interval overlapping
                        
            return Created("", workingIntervalId);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllWorkingIntervals()
        {
            var query = new GetAllWorkingIntervalsQuery();

            var allWorkingIntervals = await _mediator.Send(query);

            if (!allWorkingIntervals.Any()) return NotFound();

            var mappedWorkingIntervals = _mapper.Map<List<WorkingIntervalGetDto>>(allWorkingIntervals);

            return Ok(mappedWorkingIntervals);
        }

        [HttpGet]
        [Route("all/{employeeId}")]
        public async Task<IActionResult> GetAllWorkingIntervalsByEmployeeId(int employeeId)
        {
            var query = new GetAllWorkingIntervalsByEmployeeIdQuery
            {
                EmployeeId = employeeId
            };

            var allWorkingIntervalsByEmployeeId = await _mediator.Send(query);

            if (!allWorkingIntervalsByEmployeeId.Any()) return NotFound();

            var mappedWorkingIntervalsByEmployeeId = _mapper.Map<List<WorkingIntervalGetDto>>(allWorkingIntervalsByEmployeeId);

            return Ok(mappedWorkingIntervalsByEmployeeId);
        }

        [HttpGet]
        [Route("all/{employeeId}/{workingDayId}")]
        public async Task<IActionResult> GetAllWorkingIntervalsByEmployeeIdByDate(int employeeId, int workingDayId)
        {
            var query = new GetAllWorkingIntervalsByEmployeeIdByDateQuery
            {
                EmployeeId = employeeId,
                WorkingDayId = workingDayId
            };

            var allWorkingIntervalsByEmployeeIdByDate = await _mediator.Send(query);

            if (!allWorkingIntervalsByEmployeeIdByDate.Any()) return NotFound();

            var mappedWorkingIntervalsByEmployeeIdByDate = _mapper.Map<List<WorkingIntervalGetDto>>(allWorkingIntervalsByEmployeeIdByDate);

            return Ok(mappedWorkingIntervalsByEmployeeIdByDate);
        }

        [HttpPut]
        [Route("{workingIntervalId}")]
        // ??? Ar trebui sa fac un DTO diferit pt. Put? Intreb pt. ca practic nu as vrea sa ii ofer la customer sa isi poata schimba customerId, nu?
        public async Task<IActionResult> UpdateWorkingInterval(int workingIntervalId, [FromBody] WorkingIntervalPutDto editedWorkingInterval)
        {
            var command = new UpdateWorkingIntervalCommand
            {
                WorkingIntervalId = workingIntervalId,
                WorkingDayId= editedWorkingInterval.WorkingDayId,
                StartTime = editedWorkingInterval.StartTime,
                EndTime = editedWorkingInterval.EndTime
            };

            var result = await _mediator.Send(command);

            if (result == null) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [Route("{workingIntervalId}")]
        public async Task<IActionResult> DeleteWorkingInterval(int workingIntervalId)
        {
            var command = new DeleteWorkingIntervalCommand { WorkingIntervalId = workingIntervalId };

            var handlerResult = await _mediator.Send(command);

            if (handlerResult == null) return NotFound();

            return NoContent();
        }
    }
}
