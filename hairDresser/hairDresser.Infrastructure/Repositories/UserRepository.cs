using hairDresser.Application.CustomExceptions;
using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Password must have at least one: uppercase letter (A, ...), digit (1, ...) and alphanumeric character (symbols: #, @, %, ...).
            // Username can't have white space.
            if (!createdUser.Succeeded) throw new ClientException("Failed to create the account!");
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            // Method 1:
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
            // Method 2, using UserManager Service, will have same result as Method 1:
            // var user2 = await _userManager.FindByIdAsync(userId);
            return user;
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
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IQueryable<User>> GetAllUsersWithCustomerRoleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<User>> GetAllUsersWithEmployeeRoleAsync()
        {
            var allUsersWithEmployeeRole = await _userManager.GetUsersInRoleAsync("employee");
            var allUsersWithEmployeeRole_Ids = allUsersWithEmployeeRole.Select(employee => employee.Id).ToList();

            var allEmployees = context.Users
                .Where(user => allUsersWithEmployeeRole_Ids.Contains(user.Id))
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
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
            context.Users.Remove(user);
        }

        // Check Password:
        public async Task<bool> CheckUserPasswordAsync(User user, string userPassword)
        {
            return await _userManager.CheckPasswordAsync(user, userPassword);
        }

        // User Roles:
        public async Task<IdentityRole> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role;
        }

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

        public async Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExistsAsync(int employeeHairServiceId)
        {
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
