using hairDresser.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace hairDresser.Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user, string userPassowrd);

        Task<IQueryable<UserWithRole>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string userId);
        Task<UserWithRole> GetUserWithRoleByIdAsync(string userId);

        Task<User> GetUserByUserNameAsync(string userName);
        Task<IList<string>> GetUserRolesAsync(User user);

        Task<IQueryable<User>> GetAllUsersWithCustomerRoleAsync();
        Task<IQueryable<User>> GetAllUsersWithEmployeeRoleAsync();

        Task<User> UpdateUserAsync(User user);

        Task DeleteUserAsync(string userId);

        Task<bool> CheckUserPasswordAsync(User user, string userPassword);


        // User Roles:
        Task CreateRoleAsync(string roleName);
        Task AddRoleToUserAsync(User user, string roleName);
        Task<IdentityRole> GetRoleByNameAsync(string roleName);

        // EmployeeHairService:
        Task AddHairServiceToEmployeeAsync(List<EmployeeHairService> employee);

        Task<IQueryable<User>> GetAllEmployeesByHairServicesIdsAsync(List<int> hairServicesIds);
        Task<List<int>> GetEmployeeHairServicesIdsAsync(string employeeId);

        Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExistsAsync(int employeeHairServiceId);

        Task DeleteHairServiceFromEmployeeAsync(int employeeHairServiceId);
    }
}
