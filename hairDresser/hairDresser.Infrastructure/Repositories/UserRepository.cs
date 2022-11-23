using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
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

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await context.Users
                .FirstOrDefaultAsync(user => user.Id.Equals(userId));
        }

        public async Task<IQueryable<ApplicationUser>> GetAllUsersAsync()
        {
            return context.Users;
        }

        // ???
        public async Task<IQueryable> GetAllCustomersByRoleAsync()
        {
            throw new NotImplementedException();
        }

        // ???
        public async Task<IQueryable> GetAllEmployeesByRoleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ApplicationUser>> GetAllEmployeesByHairServicesAsync(List<int> hairServicesIds)
        {
            var validEmployees = context.Users
                .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
                .ThenInclude(hairServices => hairServices.HairService)
                .ToList()
                .Where(employee => hairServicesIds.All(serviceId => employee.EmployeeHairServices.Any(hairservice => hairservice.HairServiceId == serviceId)));
            return validEmployees.AsQueryable();
        }

        public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser user)
        {
            context.Users.Update(user);
            return user;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
            context.Users.Remove(user);
        }

        // EmployeeHairService:
        public async Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExistsAsync(int employeeHairServiceId)
        {
            return await context.EmployeesHairServices
                .FirstOrDefaultAsync(employeeHairService => employeeHairService.Id == employeeHairServiceId);
        }

        public async Task AddHairServiceToEmployeeAsync(List<EmployeeHairService> employee)
        {
            await context.EmployeesHairServices.AddRangeAsync(employee);
        }

        public async Task DeleteHairServiceFromEmployeeAsync(int employeeHairServiceId)
        {
            var employeeHairService = await context.EmployeesHairServices
                .FirstOrDefaultAsync(employeeHairService => employeeHairService.Id == employeeHairServiceId);
            context.EmployeesHairServices.Remove(employeeHairService);
        }
    }
}
