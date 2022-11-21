//using AutoMapper;
//using hairDresser.Application.Customers.Commands.CreateCustomer;
//using hairDresser.Application.Customers.Commands.DeleteCustomer;
//using hairDresser.Application.Customers.Commands.UpdateCustomer;
//using hairDresser.Application.Customers.Queries.GetAllCustomers;
//using hairDresser.Application.Customers.Queries.GetCustomerById;
//using hairDresser.Presentation.Dto.CustomerDtos;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace hairDresser.Presentation.Controllers
//{
//    [ApiController]
//    [Route("api/customer")]
//    public class CustomerController : ControllerBase
//    {
//        public readonly IMapper _mapper;
//        public readonly IMediator _mediator;

//        public CustomerController(IMapper mapper, IMediator mediator)
//        {
//            _mediator = mediator;
//            _mapper = mapper;
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerPostPutDto customerInput)
//        {
//            var command = _mapper.Map<CreateCustomerCommand>(customerInput);

//            var customer = await _mediator.Send(command);

//            var mappedCustomer = _mapper.Map<CustomerGetDto>(customer);

//            return CreatedAtAction(nameof(GetCustomerById),
//                new { id = mappedCustomer.Id },
//                mappedCustomer);
//        }

//        [HttpGet]
//        [Route("all")]
//        public async Task<IActionResult> GetAllCustomers()
//        {
//            var query = new GetAllCustomersQuery();

//            var allCustomers = await _mediator.Send(query);

//            if (!allCustomers.Any()) return NotFound();

//            var mappedCustomers = _mapper.Map<List<CustomerGetDto>>(allCustomers);

//            return Ok(mappedCustomers);
//        }

//        [HttpGet]
//        [Route("{id}")]
//        public async Task<IActionResult> GetCustomerById(int id)
//        {
//            var query = new GetCustomerByIdQuery { Id = id };

//            var customer = await _mediator.Send(query);

//            if (customer == null) return NotFound();

//            var mappedCustomer = _mapper.Map<CustomerGetDto>(customer);

//            return Ok(mappedCustomer);
//        }

//        [HttpPut]
//        [Route("{id}")]
//        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerPostPutDto editedCustomer)
//        {
//            var command = new UpdateCustomerCommand
//            {
//                Id = id,
//                Name = editedCustomer.Name,
//                Username = editedCustomer.Username,
//                Password = editedCustomer.Password,
//                Email = editedCustomer.Email,
//                Phone = editedCustomer.Phone,
//                Address = editedCustomer.Address
//            };

//            var result = await _mediator.Send(command);

//            if (result == null) return NotFound();

//            return NoContent();
//        }

//        [HttpDelete]
//        [Route("{id}")]
//        public async Task<IActionResult> DeleteCustomer(int id)
//        {
//            var command = new DeleteCustomerCommand { Id = id };

//            var handlerResult = await _mediator.Send(command);

//            if (handlerResult == null) return NotFound();

//            return NoContent();
//        }
//    }
//}
