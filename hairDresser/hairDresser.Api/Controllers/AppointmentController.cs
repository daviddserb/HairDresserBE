using AutoMapper;
using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Commands.DeleteAppointment;
using hairDresser.Application.Appointments.Commands.ReviewAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId;
using hairDresser.Application.Appointments.Queries.GetAllAppointmentsByEmployeeId;
using hairDresser.Application.Appointments.Queries.GetAppointmentById;
using hairDresser.Application.Appointments.Queries.GetFinishedAppointmentsByCustomerId;
using hairDresser.Application.Appointments.Queries.GetFinishedAppointmentsByEmployeeId;
using hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByCustomerId;
using hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByEmployeeId;
using hairDresser.Presentation.Dto.AppointmentDtos;
using hairDresser.Presentation.Dto.ReviewDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Customers Create Appointments
        /// </summary>
        /// <param name="appointmentInput"></param>
        /// <returns>Returns the created appointment</returns>
        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentPostDto appointmentInput)
        {
            _logger.LogInformation("Start process: Create appointment...");
            // Map the object type, which it changes the type from AppointmentPostDto to CreateAppointmentCommand and match (map) as well the properties, by data type and name.
            var command = _mapper.Map<CreateAppointmentCommand>(appointmentInput);

            // Send() method calls the Handler => will have the result (return) from the Handle method.
            var appointment = await _mediator.Send(command);

            var mappedAppointment = _mapper.Map<AppointmentGetDto>(appointment);

            _logger.LogInformation("Finish process: Appointment successfully created.");
            return CreatedAtAction(nameof(GetAppointmentById), new {appointmentId = mappedAppointment.Id}, mappedAppointment);
        }

        ///<summary>
        ///Get All Users from the Database.
        ///</summary>
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllAppointments([FromQuery] GetAllAppointmentsQuery paginationQuery)
        {
            var allAppointments = await _mediator.Send(paginationQuery);

            var mappedAllAppointments = _mapper.Map<List<AppointmentGetDto>>(allAppointments);

            return Ok(mappedAllAppointments);
        }

        [HttpGet]
        [Route("{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var query = new GetAppointmentByIdQuery { AppointmentId = appointmentId };

            var appointment = await _mediator.Send(query);

            var mappedAppointment = _mapper.Map<AppointmentGetDto>(appointment);

            return Ok(mappedAppointment);
        }

        [HttpGet]
        [Route("all/customer/{customerId}")]
        public async Task<IActionResult> GetAppointmentsByCustomerId(string customerId)
        {
            var query = new GetAllAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var allCustomerAppointments = await _mediator.Send(query);

            var mappedCustomerAppointments = _mapper.Map<List<AppointmentGetDto>>(allCustomerAppointments);

            return Ok(mappedCustomerAppointments);
        }

        [HttpGet]
        [Route("finished/customer/{customerId}")]
        public async Task<IActionResult> GetFinishedAppointmentsByCustomerId(string customerId)
        {
            var query = new GetFinishedAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var customerFinishedAppointments = await _mediator.Send(query);

            var mappedCustomerFinishedAppointments = _mapper.Map<List<AppointmentGetDto>>(customerFinishedAppointments);

            return Ok(mappedCustomerFinishedAppointments);
        }

        [HttpGet]
        [Route("in-work/customer/{customerId}")]
        public async Task<IActionResult> GetInWorkAppointmentsByCustomerId(string customerId)
        {
            var query = new GetInWorkAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var customerInWorkAppointments = await _mediator.Send(query);

            var mappedCustomerInWorkAppointments = _mapper.Map<List<AppointmentGetDto>>(customerInWorkAppointments);

            return Ok(mappedCustomerInWorkAppointments);
        }

        [HttpGet]
        [Route("all/employee/{employeeId}")]
        public async Task<IActionResult> GetAppointmentsByEmployeeId(string employeeId)
        {
            var query = new GetAllAppointmentsByEmployeeIdQuery { EmployeeId = employeeId };

            var allEmployeeAppointments = await _mediator.Send(query);

            var mappedEmployeeAppointments = _mapper.Map<List<AppointmentGetDto>>(allEmployeeAppointments);

            return Ok(mappedEmployeeAppointments);
        }

        [HttpGet]
        [Route("finished/employee/{employeeId}")]
        public async Task<IActionResult> GetFinishedAppointmentsByEmployeeId(string employeeId)
        {
            var query = new GetFinishedAppointmentsByEmployeeIdQuery { EmployeeId = employeeId };

            var employeeFinishedAppointments = await _mediator.Send(query);

            var mappedEmployeeFinishedAppointments = _mapper.Map<List<AppointmentGetDto>>(employeeFinishedAppointments);

            return Ok(mappedEmployeeFinishedAppointments);
        }

        [HttpGet]
        [Route("in-work/employee/{employeeId}")]
        public async Task<IActionResult> GetInWorkAppointmentsByEmployeeId(string employeeId)
        {
            var query = new GetInWorkAppointmentsByEmployeeIdQuery { EmployeeId = employeeId };

            var employeeInWorkAppointments = await _mediator.Send(query);

            var mappedEmployeeInWorkAppointments = _mapper.Map<List<AppointmentGetDto>>(employeeInWorkAppointments);

            return Ok(mappedEmployeeInWorkAppointments);
        }

        [HttpPost]
        [Route("{appointmentId}/review")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> ReviewAppointment(int appointmentId, [FromBody] ReviewPostDto reviewInput)
        {
            var command = new ReviewAppointmentCommand
            {
                CustomerId = reviewInput.CustomerId,
                AppointmentId = appointmentId,
                Rating = reviewInput.Rating,
                Comments = reviewInput.Comments
            };

            var appointmentReviewed = await _mediator.Send(command);

            var mappedAppointmentReviewed = _mapper.Map<AppointmentGetDto>(appointmentReviewed);

            return Ok(mappedAppointmentReviewed);
        }
        
        [HttpDelete]
        [Route("{customerId}/{appointmentId}")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> DeleteAppointment(string customerId, int appointmentId)
        {
            var command = new DeleteAppointmentCommand {
                CustomerId = customerId,
                AppointmentId = appointmentId
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}