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
        void CreateEmployee(Employee employee);
        Employee GetEmployeeById(int employeeId);
        IEnumerable<Employee> GetEmployeesByServices(List<string> servicesPickedByCustomer);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
