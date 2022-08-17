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

        public async Task CreateEmployeeAsync(Employee employee)
        {
            EmployeeList.Add(employee);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return EmployeeList.FirstOrDefault(obj => obj.Id == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByServicesAsync(List<string> servicesPickedByCustomer)
        {
            // ???
            // Am prea multa logica sau nu? Nu stiu exact ce sa fac, pt. ca dupa cum am denumit functia, eu trebuie sa returnez toti employee din lista care au toate serviciile cerute de customer.
            var validEmployees = new List<Employee>();
            foreach(var employee in EmployeeList)
            {
                var employeeSpecialization = employee.Specialization.ToUpperInvariant();
                bool matches = servicesPickedByCustomer.All(services => employeeSpecialization.Contains(services.ToUpperInvariant()));
                if (matches)
                {
                    validEmployees.Add(employee);
                }
            }
            return validEmployees;
        }

        public async Task<IEnumerable<Employee>> ReadEmployeesAsync()
        {
            return EmployeeList;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = EmployeeList.SingleOrDefault(obj => obj.Id == employeeId);
            if (employee != null)
            {
                EmployeeList.Remove(employee);
            }
        }
    }
}
