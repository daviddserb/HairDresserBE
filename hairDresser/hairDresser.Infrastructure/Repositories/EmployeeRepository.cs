using hairDresser.Application.Interfaces;
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
            EmployeeList.Add(employee);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return EmployeeList.FirstOrDefault(obj => obj.Id == employeeId);
        }

        public IEnumerable<Employee> GetEmployeesByServices(List<string> servicesPickedByCustomer)
        {
            var validEmployees = new List<Employee>();
            foreach(var employee in EmployeeList)
            {
                var invariantText = employee.Specialization.ToUpperInvariant();
                bool matches = servicesPickedByCustomer.All(hspbc => invariantText.Contains(hspbc.ToUpperInvariant()));
                if (matches)
                {
                    validEmployees.Add(employee);
                }
            }

            Console.WriteLine("Repository -> ");

            if (validEmployees.Count() == 0)
            {
                Console.WriteLine("Nobody can help you.");
            }
            else
            {
                Console.WriteLine("All the employees that can help you:");
                for (int i = 0; i < validEmployees.Count(); ++i)
                {
                    Console.WriteLine(validEmployees[i].Id + " - " + validEmployees[i].Name);
                }
            }

            return validEmployees;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return EmployeeList;
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = EmployeeList.SingleOrDefault(obj => obj.Id == employeeId);
            if (employee != null)
            {
                EmployeeList.Remove(employee);
            }
        }
    }
}
