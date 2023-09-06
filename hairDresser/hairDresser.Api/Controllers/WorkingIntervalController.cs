using AutoMapper;
using hairDresser.Application.WorkingIntervals.Commands.CreateWorkingInterval;
using hairDresser.Application.WorkingIntervals.Commands.DeleteWorkingInterval;
using hairDresser.Application.WorkingIntervals.Commands.UpdateWorkingInterval;
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

            var mappedWorkingInterval = _mapper.Map<WorkingIntervalGetDto>(workingInterval);

            return CreatedAtAction(nameof(GetWorkingIntervalById), new { workingIntervalId = mappedWorkingInterval.Id }, mappedWorkingInterval);
        }

        [HttpGet]
        [Route("{workingIntervalId}")]
        public async Task<IActionResult> GetWorkingIntervalById(int workingIntervalId)
        {
            var query = new GetWorkingIntervalByIdQuery { WorkingIntervalId = workingIntervalId };

            var workingInterval = await _mediator.Send(query);

            var mappedWorkingInterval = _mapper.Map<WorkingIntervalGetDto>(workingInterval);

            return Ok(mappedWorkingInterval);
        }

        [HttpGet]
        [Route("all/{employeeId}")]
        public async Task<IActionResult> GetAllWorkingIntervalsByEmployeeId(string employeeId)
        {
            var query = new GetAllWorkingIntervalsByEmployeeIdQuery{ EmployeeId = employeeId };

            var employeeWorkingIntervals = await _mediator.Send(query);

            var mappedEmployeeWorkingIntervals = _mapper.Map<List<WorkingIntervalGetDto>>(employeeWorkingIntervals);

            return Ok(mappedEmployeeWorkingIntervals);
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

            // to do ??? !!!: sa modific WorkingIntervalGetDto, probabil sa sterg public EmployeeGetDto Employee { get; set; } si sa pun doar string numele/id Employee
            var mappedWorkingIntervalsByEmployeeIdByDate = _mapper.Map<List<WorkingIntervalGetDto>>(allWorkingIntervalsByEmployeeIdByDate);

            return Ok(mappedWorkingIntervalsByEmployeeIdByDate);
        }

        [HttpPut]
        [Route("{workingIntervalId}")]
        public async Task<IActionResult> UpdateWorkingInterval(int workingIntervalId, [FromBody] WorkingIntervalPutDto editedWorkingInterval)
        {
            // ??? if I update with a invalid working interval it's still saved in the database.
            var command = new UpdateWorkingIntervalCommand
            {
                WorkingIntervalId = workingIntervalId,
                WorkingDayId= editedWorkingInterval.WorkingDayId,
                StartTime = editedWorkingInterval.StartTime,
                EndTime = editedWorkingInterval.EndTime
            };

            var workingIntervalUpdated = await _mediator.Send(command);

            var mappedWorkingIntervalUpdated = _mapper.Map<WorkingIntervalGetDto>(workingIntervalUpdated);

            return Ok(mappedWorkingIntervalUpdated);
        }

        [HttpDelete]
        [Route("{workingIntervalId}")]
        public async Task<IActionResult> DeleteWorkingInterval(int workingIntervalId)
        {
            var command = new DeleteWorkingIntervalCommand { WorkingIntervalId = workingIntervalId };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}