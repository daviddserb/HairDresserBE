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
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetEmployeesByServicesAsync(List<string> servicesPickedByCustomer);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
    }
}
