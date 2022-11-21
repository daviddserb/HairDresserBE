using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(string userId);
        Task<IQueryable<User>> GetAllUsersAsync();
        Task<IQueryable> GetAllCustomersByRoleAsync();
        Task<IQueryable> GetAllEmployeesByRoleAsync();
        Task<IQueryable<User>> GetAllEmployeesByHairServicesAsync(List<int> hairServicesIds);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(string userId);

        // EmployeeHairService:
        Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExistsAsync(int employeeHairServiceId);
        Task AddHairServiceToEmployeeAsync(List<EmployeeHairService> employee);
        Task DeleteHairServiceFromEmployeeAsync(int employeeHairServiceId);
    }
}
