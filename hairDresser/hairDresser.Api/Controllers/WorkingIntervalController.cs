using AutoMapper;
using hairDresser.Application.WorkingIntervals.Commands.CreateWorkingInterval;
using hairDresser.Application.WorkingIntervals.Commands.DeleteWorkingInterval;
using hairDresser.Application.WorkingIntervals.Commands.UpdateWorkingInterval;
using hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervals;
using hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeId;
using hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervalsByEmployeeIdByDate;
using hairDresser.Application.WorkingIntervals.Queries.GetWorkingIntervalById;
using hairDresser.Presentation.Dto.WorkingIntervalDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    [Route("api/working-interval")]
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
        public async Task<IActionResult> CreateWorkingIntervalAsync([FromBody] WorkingIntervalPostDto workingIntervalInput)
        {
            var command = _mapper.Map<CreateWorkingIntervalCommand>(workingIntervalInput);

            var workingInterval = await _mediator.Send(command);

            // Working interval overlapping or not minimum 1 hour pause between them.
            if (workingInterval.WorkingDay == null) return BadRequest();

            var mappedWorkingInterval = _mapper.Map<WorkingIntervalGetDto>(workingInterval);

            return CreatedAtAction(nameof(GetWorkingIntervalById),
                new { workingIntervalId = mappedWorkingInterval.Id },
                mappedWorkingInterval);
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
        [Route("{workingIntervalId}")]
        public async Task<IActionResult> GetWorkingIntervalById(int workingIntervalId)
        {
            var query = new GetWorkingIntervalByIdQuery { WorkingIntervalId = workingIntervalId };

            var workingInterval = await _mediator.Send(query);

            if (workingInterval == null) return NotFound();

            var mappedWorkingInterval = _mapper.Map<WorkingIntervalGetDto>(workingInterval);

            return Ok(mappedWorkingInterval);
        }

        [HttpGet]
        [Route("all/{employeeId}")]
        public async Task<IActionResult> GetAllWorkingIntervalsByEmployeeId(string employeeId)
        {
            var query = new GetAllWorkingIntervalsByEmployeeIdQuery{ EmployeeId = employeeId };

            var allWorkingIntervalsByEmployeeId = await _mediator.Send(query);

            if (!allWorkingIntervalsByEmployeeId.Any()) return NotFound("The employee has no working intervals!");

            var mappedWorkingIntervalsByEmployeeId = _mapper.Map<List<WorkingIntervalGetDto>>(allWorkingIntervalsByEmployeeId);

            return Ok(mappedWorkingIntervalsByEmployeeId);
        }

        [HttpGet]
        [Route("all/{employeeId}/{workingDayId}")]
        public async Task<IActionResult> GetAllWorkingIntervalsByEmployeeIdByDate(string employeeId, int workingDayId)
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
