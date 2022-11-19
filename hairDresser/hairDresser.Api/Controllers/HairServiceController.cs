using AutoMapper;
using hairDresser.Application.HairServices.Commands.CreateHairService;
using hairDresser.Application.HairServices.Commands.DeleteHairService;
using hairDresser.Application.HairServices.Commands.UpdateHairService;
using hairDresser.Application.HairServices.Queries;
using hairDresser.Application.HairServices.Queries.GetAllHairServicesByEmployeeId;
using hairDresser.Application.HairServices.Queries.GetAllHairServicesByIds;
using hairDresser.Application.HairServices.Queries.GetDurationByHairServicesIds;
using hairDresser.Application.HairServices.Queries.GetHairServiceById;
using hairDresser.Application.HairServices.Queries.GetMissingHairServicesByEmployeeId;
using hairDresser.Application.HairServices.Queries.GetPriceByHairServicesIds;
using hairDresser.Presentation.Dto.HairServiceDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    [Route("api/hairservice")]
    public class HairServiceController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public HairServiceController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHairServiceAsync([FromBody] HairServicePostPutDto hairServiceInput)
        {
            var command = _mapper.Map<CreateHairServiceCommand>(hairServiceInput);

            var hairService = await _mediator.Send(command);

            var mappedHairService = _mapper.Map<HairServiceGetDto>(hairService);

            return CreatedAtAction(nameof(GetHairServiceById),
                new { id = mappedHairService.Id },
                mappedHairService);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllHairServices()
        {
            var query = new GetAllHairServicesQuery();

            var allHairServices = await _mediator.Send(query);

            if (!allHairServices.Any()) return NotFound();

            var mappedHairServices = _mapper.Map<List<HairServiceGetDto>>(allHairServices);

            return Ok(mappedHairServices);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHairServiceById(int id)
        {
            var query = new GetHairServiceByIdQuery { Id = id };

            var hairService = await _mediator.Send(query);

            if (hairService == null) return NotFound();

            var mappedhairService = _mapper.Map<HairServiceGetDto>(hairService);

            return Ok(mappedhairService);
        }

        [HttpGet]
        [Route("all/employee/{employeeId}")]
        public async Task<IActionResult> GetHairServicesByEmployeeId(string employeeId)
        {
            var query = new GetAllHairServicesByEmployeeIdQuery { EmployeeId = employeeId };

            var employeeHairServices = await _mediator.Send(query);

            var mappedEmployeeHairServices = _mapper.Map<List<HairServiceGetDto>>(employeeHairServices);

            return Ok(mappedEmployeeHairServices);
        }

        [HttpGet]
        [Route("missing/employee/{employeeId}")]
        public async Task<IActionResult> GetMissingHairServicesByEmployeeId(string employeeId)
        {
            var query = new GetMissingHairServicesByEmployeeIdQuery { EmployeeId = employeeId };

            var employeeMissingHairServices = await _mediator.Send(query);

            var mappedEmployeeMissingHairServices = _mapper.Map<List<HairServiceGetDto>>(employeeMissingHairServices);

            return Ok(mappedEmployeeMissingHairServices);
        }

        [HttpGet]
        [Route("all/by-ids")]
        public async Task<IActionResult> GetAllHairServicesByIds([FromQuery] List<int> ids)
        {
            var query = new GetAllHairServicesByIdsQuery { HairServicesIds = ids };

            var hairServices = await _mediator.Send(query);

            if (hairServices == null) return NotFound();

            var mappedhairServices = _mapper.Map<List<HairServiceGetDto>>(hairServices);

            return Ok(mappedhairServices);
        }

        [HttpGet]
        [Route("duration/by-ids")]
        public async Task<IActionResult> GetDurationByHairServicesIds([FromQuery] List<int> hairServicesIds)
        {
            var query = new GetDurationByHairServicesIdsQuery { HairServicesIds = hairServicesIds };

            var durationHairServices = await _mediator.Send(query);

            return Ok(durationHairServices);
        }

        [HttpGet]
        [Route("price/by-ids")]
        public async Task<IActionResult> GetPriceByHairServicesIds([FromQuery] List<int> hairServicesIds)
        {
            var query = new GetPriceByHairServicesIdsQuery { HairServicesIds = hairServicesIds };

            var priceHairServices = await _mediator.Send(query);

            return Ok(priceHairServices);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHairService(int id, [FromBody] HairServicePostPutDto editedHairService)
        {
            var command = new UpdateHairServiceCommand
            {
                Id = id,
                Name = editedHairService.Name,
                DurationInMinutes = editedHairService.DurationInMinutes,
                Price = (float)editedHairService.Price
            };


            var result = await _mediator.Send(command);

            if (result == null) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHairService(int id)
        {
            var command = new DeleteHairServiceCommand { Id = id };

            var handlerResult = await _mediator.Send(command);

            if (handlerResult == null) return NotFound();

            return NoContent();
        }
    }
}
