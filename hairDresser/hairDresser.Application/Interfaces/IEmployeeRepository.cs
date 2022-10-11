using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task CreateEmployeeAsync(Employee employee);
        Task<IQueryable<Employee>> ReadEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<IQueryable<Employee>> GetAllEmployeesByServicesAsync(List<int> hairServicesIds);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);

        // EmployeeHairService:
        Task<EmployeeHairService> CheckIfEmployeeHairServiceIdExists(int employeeHairServiceId);
        Task UpdateEmployeeHairServicesAsync(Employee employee);
        Task DeleteEmployeeHairServiceAsync(int employeeHairServiceId);
    }
}