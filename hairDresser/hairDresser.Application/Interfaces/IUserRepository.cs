using hairDresser.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user, string userPassowrd);

        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<IQueryable<UserWithRole>> GetAllUsersAsync();
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<IQueryable<User>> GetAllUsersWithCustomerRoleAsync();
        Task<IQueryable<User>> GetAllUsersWithEmployeeRoleAsync();

        Task<User> UpdateUserAsync(User user);

        Task DeleteUserAsync(string userId);

        // Check Password:
        Task<bool> CheckUserPasswordAsync(User user, string userPassword);

        // User Roles:
        Task<IdentityRole> GetRoleByNameAsync(string roleName);
        Task CreateRoleAsync(string roleName);
        Task AddRoleToUserAsync(User user, string roleName);

        // EmployeeHairService:
        Task<IQueryable<User>> GetAllEmployeesByHairServicesIdsAsync(List<int> hairServicesIds);
        Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExistsAsync(int employeeHairServiceId);
        Task AddHairServiceToEmployeeAsync(List<EmployeeHairService> employee);
        Task DeleteHairServiceFromEmployeeAsync(int employeeHairServiceId);
    }
}
