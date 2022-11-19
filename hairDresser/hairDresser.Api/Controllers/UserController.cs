using AutoMapper;
using hairDresser.Application.Users.Queries.GetAllUsers;
using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //??? Sa vad prin ce trimit userInfo, FromQuery, FromBody, fara nimic si sa vad diferentele.
        public async Task<IActionResult> Register(UserRegisterDto userInfo)
        {
            var userExists = await _userManager.FindByNameAsync(userInfo.Username);

            if (userExists != null) return BadRequest("User already exists.");

            var user = new User
            {
                UserName = userInfo.Username, // We need the UserName.
                Address = userInfo.Address, // We need the Address because I added this column in the User table.
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

                //G enerate the key (which is the same as the key from the Program.cs).
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
        [Route("all/customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            // !!! NO:
            //var allCustomers = await _roleManager.FindByNameAsync("customer");
            //var allEmployees = await _roleManager.FindByNameAsync("Employee");
            //var allAdmins = await _roleManager.FindByNameAsync("admin");

            // ???
            //var x = _userManager.Users.Include(u => u.Roles);

            // ???
            //var list = new List<User>();
            //foreach (var user in _userManager.Users.ToList())
            //{
            //    list.Add(new User()
            //    {
            //        Roles = await _userManager.GetRolesAsync(user)
            //    });
            //}


            return Ok();
        }

        // !!! to do:
        // get user by id
        // get all customers by role
        // get all employees by role
    }
}
