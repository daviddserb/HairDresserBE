using AutoMapper;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId;
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

        //[HttpPost]
        //public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentPostDto appointment)
        //{
        //    return appointment;
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllAppointmentsQuery();

            var result = await _mediator.Send(query);

            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{customerId}")]
        // ??? Nu trebuie sa ii pun la sf. numelui metodei Async?
        public async Task<IActionResult> GetAppointmentsByCustomerId(int customerId)
        {
            var query = new GetAllAppointmentsByCustomerIdQuery { CustomerId = customerId };

            var result = await _mediator.Send(query);

            if (!result.Any()) return NotFound();

            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(result);

            return Ok(mappedResult);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAppointmentsByEmployeeId(int employeeId)
    }
}
