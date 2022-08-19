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
        Task<Employee> GetEmployeeAsync(int employeeId);
        Task<IEnumerable<Employee>> GetEmployeesAsync(string servicesPickedByCustomer);
        Task<IEnumerable<Employee>> ReadEmployeesAsync();
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
    }
}
