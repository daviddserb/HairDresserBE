using hairDresser.Application.Interfaces;
using hairDresser.Domain;
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
        private readonly DataContext context;

        public EmployeeRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return context.Employees.First(obj => obj.Id == employeeId);
        }

        public async Task<IQueryable<Employee>> GetEmployeesAsync(string services)
        {
            var validEmployees = context.Employees.Where(obj => obj.Specialization.Contains(services));
            return validEmployees;
        }

        public async Task<IQueryable<Employee>> ReadEmployeesAsync()
        {
            // ??? Aici nu ar trebui sa folosesc await, cand ii extrag din tabela? Daca il pun am eroare. Daca nu mai trebuie, atunci o mai las async?
            return context.Employees;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                var employee = context.Employees.Single(e => e.Id == employeeId);
                context.Remove(employee);
                await context.SaveChangesAsync();
            } catch
            {
                Console.WriteLine("Nu exista employee-ul cu acel id.");
            }
        }
    }
}
