using AutoMapper;
using hairDresser.Application.Users.Commands.AddHairServicesToEmployee;
using hairDresser.Application.Users.Commands.DeleteEmployeeHairService;
using hairDresser.Application.Users.Commands.DeleteUser;
using hairDresser.Application.Users.Commands.UpdateUser;
using hairDresser.Application.Users.Queries.GetAllUsers;
using hairDresser.Application.Users.Queries.GetEmployeeFreeIntervalsByDate;
using hairDresser.Application.Users.Queries.GetEmployeesByHairServices;
using hairDresser.Application.Users.Queries.GetUserById;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.EmployeeDtos;
using hairDresser.Presentation.Dto.EmployeeFreeIntervalsDtos;
using hairDresser.Presentation.Dto.EmployeeHairServiceDtos;
using hairDresser.Presentation.Dto.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hairDresser.Presentation.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMediator mediator, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterDto userInfo)
        {
            var userExists = await _userManager.FindByNameAsync(userInfo.Username);

            if (userExists != null) return BadRequest("User already exists.");

            var user = new User
            {
                UserName = userInfo.Username,
                Email = userInfo.Email,
                PhoneNumber = userInfo.Phone,
                Address = userInfo.Address,
            };

            var result = await _userManager.CreateAsync(user, userInfo.Password);

            if (!result.Succeeded)
            {
                // OAuth has some validations on its own columns.
                // For example the password must have at least one: uppercase letter (A, ...), digit (1, ...) and alphanumeric character (symbols: #, @, %, ...).
                return BadRequest("Failed to create user.");
            }

            // ??? Can't send message inside the Ok() method. I need to find a new method like Ok where I can send a message. 
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto userInfo)
        {
            var user = await _userManager.FindByNameAsync(userInfo.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, userInfo.Password))
            {
                // If the user exists, after the login, we need to return a token, and to do that we need to add some Claims.

                var userRoles = await _userManager.GetRolesAsync(user);

                // Will be visible in the token.
                var authClaims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Name, userInfo.Name), // ??? am scos Name-ul din auth
                    new Claim("username", userInfo.Username),
                    new Claim("password", userInfo.Password)
                };

                // Add the roles, from the DB, from the specific user, to the claims.
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Generate the key (which is the same as the key from the Program.cs).
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdee-312423d-dsa213321"));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7192", // back-end
                    audience: "https://localhost:4200", // front-end
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(10),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                return Ok(new
                {
                    username = userInfo.Username,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized("Account doesn't exist.");
        }

        [HttpPost]
        [Route("assign-role")]
        public async Task<IActionResult> AddToRole(UserRoleDto userInfo)
        {
            var userExists = await _userManager.FindByNameAsync(userInfo.Username);

            if (userExists == null) return BadRequest("User does not exist.");

            var role = await _roleManager.FindByNameAsync(userInfo.Role);
        
            if (role == null)
            {
                var roleAdded = await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = userInfo.Role
                });
            }

            var addRoleToUser = await _userManager.AddToRoleAsync(userExists, userInfo.Role);

            if (!addRoleToUser.Succeeded)
            {
                return BadRequest("Failed to add user to role.");
            }

            return Ok($"The role '{userInfo.Role}' is now assigned to the user with the username '{userInfo.Username}'");
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

            var result = await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var allUsers = await _mediator.Send(query);

            if (!allUsers.Any()) return NotFound();

            var mappedAllUsers = _mapper.Map<List<UserGetDto>>(allUsers);

            return Ok(mappedAllUsers);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var query = new GetUserByIdQuery { UserId = id};

            var user = await _mediator.Send(query);

            if (user == null) return NotFound();

            var mappedUser = _mapper.Map<UserGetDto>(user);

            return Ok(mappedUser);
        }

        [HttpGet]
        [Route("customer/all")]
        public async Task<IActionResult> GetAllUsersWithCustomerRole()
        {
            // ???
            return Ok();
        }

        [HttpGet]
        [Route("employee/all")]
        public async Task<IActionResult> GetAllUsersWithEmployeeRole()
        {
            // ???
            return Ok();
        }

        [HttpGet]
        [Route("employee/all/by-hair-services")]
        public async Task<IActionResult> GetEmployeesByHairServices([FromQuery] List<int> hairServicesIds)
        {
            var query = new GetEmployeesByHairServicesQuery(hairServicesIds);

            var validEmployees = await _mediator.Send(query);

            if (!validEmployees.Any()) return NotFound();

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

            if (!freeIntervals.Any()) return NotFound();

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

            var result = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var command = new DeleteUserCommand { UserId = id };

            var handlerResult = await _mediator.Send(command);

            if (handlerResult == null) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [Route("employee/hair-service/{hairServiceId}")]
        public async Task<IActionResult> DeleteHairServiceFromEmployee(int hairServiceId)
        {
            var command = new DeleteEmployeeHairServiceCommand { EmployeeHairServiceId = hairServiceId };

            var handlerResult = await _mediator.Send(command);

            return NoContent();
        }
    }
}
