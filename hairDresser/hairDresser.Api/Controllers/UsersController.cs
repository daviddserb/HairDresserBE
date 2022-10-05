using hairDresser.Infrastructure;
using hairDresser.Presentation.Dto.UserDtos;
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
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("register")]
        // ! Sa vad prin ce trimit userInfo, FromQuery, FromBody, fara nimic si sa vad diferentele.
        public async Task<IActionResult> Register(UserLoginDto userInfo)
        {
            var userExists = await _userManager.FindByNameAsync(userInfo.Username);

            if (userExists != null) return BadRequest("User already exists.");

            var user = new User
            {
                UserName = userInfo.Username,
            };

            // ???
            var result = await _userManager.CreateAsync(user, userInfo.Password);

            if (!result.Succeeded)
            {
                // OAuth has some validations on the password, it needs to be a strong one, that means to have at least one: uppercase letter and alphanumeric character (symbols: #, @, %, ...).
                return BadRequest("Failed to create user because the password is not strong enough.");
            }

            //return Ok("User created successfully."); //before
            return Ok(); //after
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
    }
}
