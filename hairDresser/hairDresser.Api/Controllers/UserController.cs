using AutoMapper;
using hairDresser.Application.Users.Commands.AddHairServicesToEmployee;
using hairDresser.Application.Users.Commands.AddRoleToUser;
using hairDresser.Application.Users.Commands.DeleteEmployeeHairService;
using hairDresser.Application.Users.Commands.DeleteUser;
using hairDresser.Application.Users.Commands.LoginUser;
using hairDresser.Application.Users.Commands.RegisterUser;
using hairDresser.Application.Users.Commands.UpdateUser;
using hairDresser.Application.Users.Queries.GetAllUsers;
using hairDresser.Application.Users.Queries.GetAllUsersWithEmployeeRole;
using hairDresser.Application.Users.Queries.GetEmployeeFreeIntervalsByDate;
using hairDresser.Application.Users.Queries.GetEmployeesByHairServices;
using hairDresser.Application.Users.Queries.GetUserById;
using hairDresser.Application.Users.Queries.GetUserWithRoleById;
using hairDresser.Presentation.Dto.EmployeeDtos;
using hairDresser.Presentation.Dto.EmployeeFreeIntervalsDtos;
using hairDresser.Presentation.Dto.EmployeeHairServiceDtos;
using hairDresser.Presentation.Dto.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterDto userInfo)
        {
            var command = _mapper.Map<RegisterUserCommand>(userInfo);

            var user = await _mediator.Send(command);

            var mappedUser = _mapper.Map<UserGetDto>(user);

            return CreatedAtAction(nameof(GetUserById), new { userId = mappedUser.Id }, mappedUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(UserLoginDto userInfo)
        {
            var command = _mapper.Map<LoginUserCommand>(userInfo);

            var user = await _mediator.Send(command);

            var mappedUser = _mapper.Map<UserGetDto>(user);

            return Ok(mappedUser);
        }

        [HttpPost]
        [Route("assign-role")]
        public async Task<IActionResult> AssignRoleToUser(UserRoleDto userInfo)
        {
            var command = _mapper.Map<AddRoleToUserCommand>(userInfo);

            await _mediator.Send(command);

            var message = $"The role '{userInfo.Role}' is now assigned to the user with the username '{userInfo.Username}'";
            return Ok(message);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var users = await _mediator.Send(query);

            var mappedUsers = _mapper.Map<List<UserGetDto>>(users);

            return Ok(mappedUsers);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var query = new GetUserByIdQuery{ UserId = userId };

            var user = await _mediator.Send(query);

            var mappedUser = _mapper.Map<UserGetDto>(user);

            return Ok(mappedUser);
        }

        [HttpGet]
        [Route("with-role/id")]
        public async Task<IActionResult> GetUserWithRoleById(string userId)
        {
            var query = new GetUserWithRoleById { UserId = userId };

            var userWithRole = await _mediator.Send(query);

            var mappedUserWithRole = _mapper.Map<UserGetDto>(userWithRole);

            return Ok(mappedUserWithRole);
        }

        [HttpGet]
        [Route("customer/all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        // ???
        [HttpGet]
        [Route("employee/all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetAllUsersWithEmployeeRoleQuery();

            var allEmployees = await _mediator.Send(query);

            var mappedAllEmployees = _mapper.Map<List<EmployeeGetDto>>(allEmployees);

            return Ok(mappedAllEmployees);
        }

        // ???
        [HttpGet]
        [Route("employee/all/by-hair-services")]
        public async Task<IActionResult> GetEmployeesByHairServices([FromQuery] List<int> hairServicesIds)
        {
            var query = new GetEmployeesByHairServicesQuery(hairServicesIds);

            var validEmployees = await _mediator.Send(query);

            var mappedValidEmployees = _mapper.Map<List<EmployeeGetDto>>(validEmployees);

            return Ok(mappedValidEmployees);
        }

        [HttpGet]
        [Route("employee/free-intervals")]
        public async Task<IActionResult> GetEmployeeFreeIntervalsByDate([FromQuery] EmployeeFreeIntervalDto employeeFreeInterval)
        {
            var query = new GetEmployeeFreeIntervalsByDateQuery
            {
                EmployeeId = employeeFreeInterval.EmployeeId,
                Year = employeeFreeInterval.Year,
                Month = employeeFreeInterval.Month,
                Date = employeeFreeInterval.Date,
                DurationInMinutes = employeeFreeInterval.DurationInMinutes,
                CustomerId = employeeFreeInterval.CustomerId
            };

            var freeIntervals = await _mediator.Send(query);

            var mappedFreeIntervals = _mapper.Map<List<EmployeeFreeIntervalsGetDto>>(freeIntervals);

            return Ok(mappedFreeIntervals);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserPutDto editedUser)
        {
            var command = new UpdateUserCommand
            {
                Id = id,
                Username = editedUser.Username,
                Email = editedUser.Email,
                Phone = editedUser.Phone,
                Address = editedUser.Address
            };

            var userEdited = await _mediator.Send(command);

            return Ok(userEdited);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var command = new DeleteUserCommand { UserId = id };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost]
        [Route("employee/hair-service")]
        public async Task<IActionResult> AddHairServicesToEmployee([FromBody] EmployeeHairServicePostDto employeeHairService)
        {
            var command = new AddHairServicesToEmployeeCommand
            {
                EmployeeId = employeeHairService.EmployeeId,
                HairServicesIds = employeeHairService.HairServicesIds
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("employee/hair-service/{hairServiceId}")]
        public async Task<IActionResult> DeleteHairServiceFromEmployee(int hairServiceId)
        {
            var command = new DeleteHairServiceFromEmployeeCommand { EmployeeHairServiceId = hairServiceId };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
