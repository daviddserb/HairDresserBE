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
        Task CreateUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IQueryable<ApplicationUser>> GetAllUsersAsync();
        Task<IQueryable> GetAllCustomersByRoleAsync();
        Task<IQueryable> GetAllEmployeesByRoleAsync();
        Task<IQueryable<ApplicationUser>> GetAllEmployeesByHairServicesAsync(List<int> hairServicesIds);
        Task<ApplicationUser> UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);

        // EmployeeHairService:
        Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExistsAsync(int employeeHairServiceId);
        Task AddHairServiceToEmployeeAsync(List<EmployeeHairService> employee);
        Task DeleteHairServiceFromEmployeeAsync(int employeeHairServiceId);
    }
}
