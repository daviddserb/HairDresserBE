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
        private readonly List<Employee> _employees = new();

        public EmployeeRepository()
        {
            _employees.Add(new Employee { Id = 1, Name = "Matei Dima", Specialization = "wash, cut" });
            _employees.Add(new Employee { Id = 2, Name = "Onofras Rica", Specialization = "cut, dye" });
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            _employees.Add(employee);
        }

        // Get Employee By Id.
        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return _employees.FirstOrDefault(obj => obj.Id == employeeId);
        }

        // Get Employee By Services.
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string servicesPickedByCustomer)
        {
            //var validEmployees = new List<Employee>();
            //foreach(var employee in _employees)
            //{
            //    //var employeeSpecialization = employee.Specialization.ToUpperInvariant(); -> nu mai folosesc .ToUpperInvariant() pt. ca nu mai pot sa pun metoda si pe services
            //    var employeeSpecialization = employee.Specialization;
            //    Console.WriteLine("employeeSpecialization= " + employeeSpecialization);
            //    bool matches = servicesPickedByCustomer.All(services => employeeSpecialization.Contains(services));
            //    if (matches)
            //    {
            //        validEmployees.Add(employee);
            //    }
            //}

            // Am simplificat algoritmul de sus:
            var validEmployees = _employees.Where(obj => obj.Specialization.Contains(servicesPickedByCustomer));

            return validEmployees;
        }

        public async Task<IEnumerable<Employee>> ReadEmployeesAsync()
        {
            return _employees;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = _employees.SingleOrDefault(obj => obj.Id == employeeId);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
    }
}
