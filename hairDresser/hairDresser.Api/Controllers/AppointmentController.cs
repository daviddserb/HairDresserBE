using AutoMapper;
using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId;
using hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByCustomerId;
using hairDresser.Presentation.Dto.AppointmentDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public AppointmentController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentPostDto appointment)
        {
            // Create the object of type CreateAppointmentCommand.
            var command = _mapper.Map<CreateAppointmentCommand>(appointment);

            // Call the Handler method.
            var appointmentId = await _mediator.Send(command);

            return Created("", appointmentId);
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllAppointmentsQuery();

            var result = await _mediator.Send(query);

            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("All/{customerId}")]
        public async Task<IActionResult> GetAppointmentsByCustomerId(int customerId)
        {
            var query = new GetAllAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var result = await _mediator.Send(query);

            if (!result.Any()) return NotFound();

            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("InWork/{customerId}")]
        public async Task<IActionResult> GetInWorkAppointmentsByCustomerId(int customerId)
        {
            var query = new GetInWorkAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var result = await _mediator.Send(query);

            if (!result.Any()) return NotFound();

            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(result);

            return Ok(mappedResult);
        }
    }
}
