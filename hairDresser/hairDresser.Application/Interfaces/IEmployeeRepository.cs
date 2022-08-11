using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        void CreateEmployee(Employee employee);
        Employee GetEmployee(int employeeId);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
