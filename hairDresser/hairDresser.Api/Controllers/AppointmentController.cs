using AutoMapper;
using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Commands.DeleteAppointment;
using hairDresser.Application.Appointments.Commands.UpdateAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId;
using hairDresser.Application.Appointments.Queries.GetAllAppointmentsByEmployeeId;
using hairDresser.Application.Appointments.Queries.GetAppointmentById;
using hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByCustomerId;
using hairDresser.Presentation.Dto.AppointmentDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentPostDto appointmentInput)
        {
            _logger.LogInformation("Start process: Create appointment...");
            Console.WriteLine("CreateAppointmentAsync");

            // Map the object type, which means change the type from AppointmentPostDto to CreateAppointmentCommand and match (map) as well the properties (by type and name).
            var command = _mapper.Map<CreateAppointmentCommand>(appointmentInput);

            // Send() method calls the Handler, so we will have the result from the Handle method.
            var appointment = await _mediator.Send(command);

            // ??? Nu cred ca mai am nevoie de asta
            //if (appointment == null)
            //{
            //    _logger.LogError("Can't create appointment because invalid data input.");
            //    return BadRequest();
            //}

            var mappedAppointment = _mapper.Map<AppointmentGetDto>(appointment);

            _logger.LogInformation("Appointment created successfully.");

            return CreatedAtAction(nameof(GetAppointmentById), new { appointmentId = mappedAppointment.Id }, mappedAppointment);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAppointments([FromQuery] GetAllAppointmentsQuery paginationQuery)
        {
            var allAppointments = await _mediator.Send(paginationQuery);

            if (!allAppointments.Any()) return NotFound();

            var mappedAllAppointments = _mapper.Map<List<AppointmentGetDto>>(allAppointments);

            return Ok(mappedAllAppointments);
        }

        [HttpGet]
        [Route("{appointmentId}")]
        [ActionName(nameof(GetAppointmentById))]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var query = new GetAppointmentByIdQuery { AppointmentId = appointmentId };

            var appointment = await _mediator.Send(query);

            if (appointment == null) return NotFound();

            var mappedAppointment = _mapper.Map<AppointmentGetDto>(appointment);

            return Ok(mappedAppointment);
        }

        [HttpGet]
        [Route("all/customer/{customerId}")]
        public async Task<IActionResult> GetAppointmentsByCustomerId(int customerId)
        {
            var query = new GetAllAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var allCustomerAppointments = await _mediator.Send(query);

            if (!allCustomerAppointments.Any()) return NotFound();

            var mappedCustomerAppointments = _mapper.Map<List<AppointmentGetDto>>(allCustomerAppointments);

            return Ok(mappedCustomerAppointments);

        }

        [HttpGet]
        [Route("in-work/customer/{customerId}")]
        public async Task<IActionResult> GetInWorkAppointmentsByCustomerId(int customerId)
        {
            var query = new GetInWorkAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var allCustomerInWorkAppointments = await _mediator.Send(query);

            if (!allCustomerInWorkAppointments.Any()) return NotFound();

            var mappedAllCustomerInWorkAppointments = _mapper.Map<List<AppointmentGetDto>>(allCustomerInWorkAppointments);

            return Ok(mappedAllCustomerInWorkAppointments);
        }

        [HttpGet]
        [Route("all/employee/{employeeId}")]
        public async Task<IActionResult> GetAppointmentsByEmployeeId(int employeeId)
        {
            var query = new GetAllAppointmentsByEmployeeIdQuery { EmployeeId = employeeId };

            var allEmployeeAppointments = await _mediator.Send(query);

            if (!allEmployeeAppointments.Any()) return NotFound();

            var mappedEmployeeAppointments = _mapper.Map<List<AppointmentGetDto>>(allEmployeeAppointments);

            return Ok(mappedEmployeeAppointments);

        }

        [HttpPut]
        [Route("{appointmentId}")]
        public async Task<IActionResult> UpdateAppointment(int appointmentId, [FromBody] AppointmentPutDto editedAppointment)
        {
            var command = new UpdateAppointmentCommand
            {
                AppointmentId = appointmentId,
                HairServicesIds = editedAppointment.HairServicesIds,
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