using AutoMapper;
using hairDresser.Application.HairServices.Commands.CreateHairService;
using hairDresser.Application.HairServices.Commands.DeleteHairService;
using hairDresser.Application.HairServices.Commands.UpdateHairService;
using hairDresser.Application.HairServices.Queries;
using hairDresser.Application.HairServices.Queries.GetAllHairServicesByIds;
using hairDresser.Application.HairServices.Queries.GetHairServiceById;
using hairDresser.Presentation.Dto.HairServiceDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> CreateHairServiceAsync([FromBody] HairServicePostPutDto hairService)
        {
            var command = _mapper.Map<CreateHairServiceCommand>(hairService);

            var hairServiceId = await _mediator.Send(command);

            return Created("", hairServiceId);
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
        [Route("all/by-ids")]
        public async Task<IActionResult> GetAllHairServicesByIds([FromQuery] List<int> ids)
        {
            var query = new GetAllHairServicesByIdsQuery { HairServicesIds = ids };

            var hairServices = await _mediator.Send(query);

            if (hairServices == null) return NotFound();

            var mappedhairServices = _mapper.Map<List<HairServiceGetDto>>(hairServices);

            return Ok(mappedhairServices);
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
