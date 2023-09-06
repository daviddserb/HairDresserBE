using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hairDresser.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateUserAsync(User user, string userPassowrd)
        {
            var createdUser = await _userManager.CreateAsync(user, userPassowrd);

            // OAuth has some validations on its own columns from the DB Table. Some of the requirements:
            // Username can't have white space.
            // Password must have at least one: uppercase letter (A, ...), digit (1, ...) and alphanumeric character (symbols: #, @, %, ...).
            if (!createdUser.Succeeded) throw new ClientException("Failed to create the account!");
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            // Method 1:
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);

            // Method 2 (same result but using IdentityUser method):
            // var user2 = await _userManager.FindByIdAsync(userId);

            return user;
        }

        public async Task<UserWithRole> GetUserWithRoleByIdAsync(string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            if (user == null) throw new NotFoundException($"The user with the id '{userId}' does not exist!");

            var userWithRole = new UserWithRole
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.PhoneNumber,
                Role = string.Join(", ", _userManager.GetRolesAsync(user).Result.ToArray())
            };
            return userWithRole;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            // userName because it's the name of the user (don't confuse it with username).
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<IQueryable<UserWithRole>> GetAllUsersAsync()
        {
            var users = _userManager.Users.Select(user => new UserWithRole
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.PhoneNumber,
                Role = string.Join(", ", _userManager.GetRolesAsync(user).Result.ToArray())
            });
            return users;
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles;
        }

        public async Task<IQueryable<User>> GetAllUsersWithCustomerRoleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<User>> GetAllUsersWithEmployeeRoleAsync()
        {
            var usersWithEmployeeRole = await _userManager.GetUsersInRoleAsync("employee");
            var usersIdsWithEmployeeRole = usersWithEmployeeRole
                .Select(employee => employee.Id)
                .ToList();

            var allEmployees = context.Users
                .Where(user => usersIdsWithEmployeeRole.Contains(user.Id))
                .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
                    .ThenInclude(hairServices => hairServices.HairService)
                .Include(employeeWorkingInterval => employeeWorkingInterval.EmployeeWorkingIntervals.OrderBy(workingDay => workingDay.WorkingDayId).ThenBy(startTime => startTime.StartTime))
                    .ThenInclude(workingDay => workingDay.WorkingDay);
            return allEmployees;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            context.Users.Update(user);
            return user;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            context.Users.Remove(user);
        }

        public async Task<bool> CheckUserPasswordAsync(User user, string userPassword)
        {
            return await _userManager.CheckPasswordAsync(user, userPassword);
        }

        // User Roles:
        public async Task CreateRoleAsync(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
            });
        }

        public async Task AddRoleToUserAsync(User user, string roleName)
        {
            var addRoleToUser = await _userManager.AddToRoleAsync(user, roleName);
            if (!addRoleToUser.Succeeded)
            {
                // This can be possible, for example, when you try to add the same role twice to the same user.
                throw new ClientException($"Failed to add the '{roleName}' role to the '{user.UserName}' user!");
            }
        }

        public async Task<IdentityRole> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        // EmployeeHairService:
        public async Task<IQueryable<User>> GetAllEmployeesByHairServicesIdsAsync(List<int> hairServicesIds)
        {
            var validEmployees = context.Users
                .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
                    .ThenInclude(hairServices => hairServices.HairService)
                .ToList()
                .Where(employee => hairServicesIds.All(serviceId => employee.EmployeeHairServices.Any(hairservice => hairservice.HairServiceId == serviceId)));
            return validEmployees.AsQueryable();
        }

        public async Task<List<int>> GetEmployeeHairServicesIdsAsync(string employeeId)
        {
            var employeeHairServicesIds = context.EmployeesHairServices
                .Where(ehs => ehs.EmployeeId == employeeId)
                .Select(ehs => ehs.HairServiceId)
                .ToList();
            return employeeHairServicesIds;
        }

        public async Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExistsAsync(int employeeHairServiceId)
        {
            // Get the hair service IDs of the specific employee
            return await context.EmployeesHairServices.FirstOrDefaultAsync(employeeHairService => employeeHairService.Id == employeeHairServiceId);
        }

        public async Task AddHairServiceToEmployeeAsync(List<EmployeeHairService> employee)
        {
            await context.EmployeesHairServices.AddRangeAsync(employee);
        }

        public async Task DeleteHairServiceFromEmployeeAsync(int employeeHairServiceId)
        {
            var employeeHairService = await context.EmployeesHairServices.FirstOrDefaultAsync(employeeHairService => employeeHairService.Id == employeeHairServiceId);
            context.EmployeesHairServices.Remove(employeeHairService);
        }
    }
}