using hairDresser.Domain.Interfaces;
using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        List<Employee> EmployeeList = new List<Employee>();

        public EmployeeRepository()
        {
            EmployeeList.Add(new Employee { Id = 1, Name = "Matei Dima", Specialization = "wash, cut" });
            EmployeeList.Add(new Employee { Id = 2, Name = "Onofras Rica", Specialization = "cut, dye" });
        }

        public void CreateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return EmployeeList.FirstOrDefault(obj => obj.Id == employeeId);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return EmployeeList;
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
