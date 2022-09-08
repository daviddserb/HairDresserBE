using AutoMapper;
using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Commands.DeleteAppointment;
using hairDresser.Application.Appointments.Commands.UpdateAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId;
using hairDresser.Application.Appointments.Queries.GetAppointmentById;
using hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByCustomerId;
using hairDresser.Presentation.Dto.AppointmentDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    //??? [Route("api/[controller]")] sau [Route("api/appointment")] ca sa fie cu litera mica cum zice in reguli?
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IMediator mediator, IMapper mapper, ILogger<AppointmentController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentPostDto appointment)
        {
            // Get an object of type AppointmentPostDto, which means that the object has the properties from its type.
            _logger.LogInformation("Start process: Create appointment...");

            // Create the object of type CreateAppointmentCommand.
            var command = _mapper.Map<CreateAppointmentCommand>(appointment);

            // Send() method calls the Handler, so we will have the result from the Handle method.
            var appointmentId = await _mediator.Send(command);

            if (appointmentId == null) 
            {
                _logger.LogError("Can't create appointment because invalid data input.");
                return BadRequest();
            }

            _logger.LogInformation("Appointment created successfully.");

            //??? N-am prea inteles cu ce ma ajuta acel string uri din Created() aici in API (am vazut ca se afla in Response headers in location)? O sa ajute cumva cu ceva pe partea de frotnend?
            return Created("dsa123###", appointmentId);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllAppointmentsQuery();

            var allAppointments = await _mediator.Send(query);

            if (!allAppointments.Any()) return NotFound();

            var mappeAllAppointments = _mapper.Map<List<AppointmentGetDto>>(allAppointments);

            return Ok(mappeAllAppointments);
        }

        [HttpGet]
        [Route("{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var query = new GetAppointmentByIdQuery { AppointmentId = appointmentId };

            var appointment = await _mediator.Send(query);

            if (appointment == null) return NotFound();

            var mappedAppointment = _mapper.Map<AppointmentGetDto>(appointment);

            return Ok(mappedAppointment);
        }

        [HttpGet]
        [Route("all/{customerId}")]
        public async Task<IActionResult> GetAppointmentsByCustomerId(int customerId)
        {
            var query = new GetAllAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var customerAppointments = await _mediator.Send(query);

            if (!customerAppointments.Any()) return NotFound();

            var mappedCustomerAppointments = _mapper.Map<List<AppointmentGetDto>>(customerAppointments);

            return Ok(mappedCustomerAppointments);
        }

        [HttpGet]
        [Route("in-work/{customerId}")]
        public async Task<IActionResult> GetInWorkAppointmentsByCustomerId(int customerId)
        {
            var query = new GetInWorkAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var result = await _mediator.Send(query);

            if (!result.Any()) return NotFound();

            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{appointmentId}")]
        public async Task<IActionResult> UpdateAppointment(int appointmentId, [FromBody] AppointmentPutDto editedAppointment)
        {
            var command = new UpdateAppointmentCommand
            {
                AppointmentId = appointmentId,
                HairServicesId = editedAppointment.HairServicesId,
                EmployeeId = editedAppointment.EmployeeId,
                StartDate = editedAppointment.StartDate,
                EndDate = editedAppointment.EndDate
            };

            var result = await _mediator.Send(command);

            if (result == null) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [Route("{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            var command = new DeleteAppointmentCommand { AppointmentId = appointmentId };

            var handlerResult = await _mediator.Send(command);

            if (handlerResult == null) return NotFound();

            return NoContent();
        }
    }
}
