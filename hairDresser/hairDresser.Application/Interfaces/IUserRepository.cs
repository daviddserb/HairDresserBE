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
        Task<IQueryable<User>> GetAllUsersWithCustomerRoleAsync();
        Task<IQueryable<User>> GetAllUsersWithEmployeeRoleAsync(List<string> employeeIds);

        Task<User> UpdateUserAsync(User user);

        Task DeleteUserAsync(string userId);

        // EmployeeHairService:
        Task<IQueryable<User>> GetAllEmployeesByHairServicesIdsAsync(List<int> hairServicesIds);
        Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExistsAsync(int employeeHairServiceId);
        Task AddHairServiceToEmployeeAsync(List<EmployeeHairService> employee);
        Task DeleteHairServiceFromEmployeeAsync(int employeeHairServiceId);
    }
}
