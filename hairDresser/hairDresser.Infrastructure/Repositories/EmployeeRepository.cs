using hairDresser.Application.Interfaces;
using hairDresser.Domain;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext context;

        public EmployeeRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
        }

        public async Task<IQueryable<Employee>> ReadEmployeesAsync()
        {
            var employees = context.Employees;
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await context.Employees
                .FirstOrDefaultAsync(employee => employee.Id == employeeId);
            return employee;
        }

        public async Task<IQueryable<Employee>> GetAllEmployeesByServicesAsync(List<int> servicesIds)
        {
            var validEmployees = context.Employees;
            return validEmployees.AsQueryable();
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            context.Employees.Update(employee);
            return employee;
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(employee => employee.Id == employeeId);
            context.Employees.Remove(employee);
        }

        // EmployeeHairService:
        public async Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExists(int employeeHairServiceId)
        {
            var employeeHairService = await context.EmployeesHairServices
                .FirstOrDefaultAsync(employeeHairService => employeeHairService.Id == employeeHairServiceId);
            return employeeHairService;
        }

        public async Task AddEmployeeHairServicesAsync(List<EmployeeHairService> employee)
        {
            await context.EmployeesHairServices.AddRangeAsync(employee);
        }

        public async Task DeleteEmployeeHairServiceAsync(int employeeHairServiceId)
        {
            var employeeHairService = await context.EmployeesHairServices
                .FirstOrDefaultAsync(employeeHairService => employeeHairService.Id == employeeHairServiceId);
            context.EmployeesHairServices.Remove(employeeHairService);
        }
    }
}
