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

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await context.Users
                .FirstOrDefaultAsync(user => user.Id.Equals(userId));
        }

        public async Task<IQueryable<User>> GetAllUsersAsync()
        {
            return context.Users;
        }

        public async Task<IQueryable<User>> GetAllUsersWithCustomerRoleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<User>> GetAllUsersWithEmployeeRoleAsync(List<string> employeeIds)
        {
            var allEmployees = context.Users
                .Where(user => employeeIds.Contains(user.Id))
                .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
                .ThenInclude(hairServices => hairServices.HairService)
                .Include
                (
                employeeWorkingInterval => employeeWorkingInterval.EmployeeWorkingIntervals
                .OrderBy(workingDay => workingDay.WorkingDayId)
                .ThenBy(startTime => startTime.StartTime)
                )
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
