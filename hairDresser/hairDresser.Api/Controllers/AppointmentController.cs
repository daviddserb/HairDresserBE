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
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentPostPutDto appointment)
        {
            // Create the object of type CreateAppointmentCommand.
            var command = _mapper.Map<CreateAppointmentCommand>(appointment);

            // Call the Handler method.
            var appointmentId = await _mediator.Send(command);

            //??? N-am prea inteles cu ce ma ajuta acel string uri din Created().
            return Created("", appointmentId);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllAppointmentsQuery();

            var allAppointments = await _mediator.Send(query);

            if (!allAppointments.Any()) return NotFound();

            var mappedAppointments = _mapper.Map<List<AppointmentGetDto>>(allAppointments);

            return Ok(mappedAppointments);
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

            var result = await _mediator.Send(query);

            if (!result.Any()) return NotFound();

            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(result);

            return Ok(mappedResult);
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
        // ??? Ar trebui sa fac un DTO diferit pt. Put? Pt. ca practic nu as vrea sa ii ofer la customer sa isi poata schimba customerId, nu?
        public async Task<IActionResult> UpdateAppointment(int appointmentId, [FromBody] AppointmentPostPutDto editedAppointment)
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
