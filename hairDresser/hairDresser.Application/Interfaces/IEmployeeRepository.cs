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
        Task<Employee> GetEmployeeAsync(int employeeId);
        Task<IQueryable<Employee>> GetEmployeesAsync(List<int> services);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
    }
}
