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
using Microsoft.AspNetCore.Authorization;
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

            return CreatedAtAction(nameof(GetHairServiceById), new { hairServiceId = mappedHairService.Id }, mappedHairService);
        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public async Task<IActionResult> GetAllHairServices()
        {
            var allHairServices = await _mediator.Send(new GetAllHairServicesQuery());

            var mappedHairServices = _mapper.Map<List<HairServiceGetDto>>(allHairServices);

            return Ok(mappedHairServices);
        }

        [HttpGet]
        [Route("{hairServiceId}")]
        public async Task<IActionResult> GetHairServiceById(int hairServiceId)
        {
            var query = new GetHairServiceByIdQuery { HairServiceId = hairServiceId };

            var hairService = await _mediator.Send(query);

            var mappedhairService = _mapper.Map<HairServiceGetDto>(hairService);

            return Ok(mappedhairService);
        }

        [HttpGet]
        [Route("all/employee/{employeeId}")]
        public async Task<IActionResult> GetAllHairServicesByEmployeeId(string employeeId)
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
        public async Task<IActionResult> GetAllHairServicesByIds([FromQuery] List<int> hairServicesIds)
        {
            var query = new GetAllHairServicesByIdsQuery { HairServicesIds = hairServicesIds };

            var hairServices = await _mediator.Send(query);

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
                Price = editedHairService.Price
            };

            var hairServiceUpdated = await _mediator.Send(command);

            var mappedHairServiceUpdated = _mapper.Map<HairServiceGetDto>(hairServiceUpdated);

            return Ok(mappedHairServiceUpdated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHairService(int id)
        {
            var command = new DeleteHairServiceCommand { HairServiceId = id };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}