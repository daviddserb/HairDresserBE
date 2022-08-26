using hairDresser.Application.Interfaces;
using hairDresser.Domain;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IQueryable<Employee>> ReadEmployeesAsync()
        {
            //BEFORE:
            //!!! Chiar daca in clasa Employee am navigational property spre alte clase (in ex. de fata WorkingIntervals 
            return context.Employees
                .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
                .ThenInclude(hairServices => hairServices.HairService);

            //AFTER (merge dar trebuie cumva sa fac GroupBy() si nu-mi dau seama cum pt. ca am erori):
            //return context.EmployeesHairServices
            //.Include(x => x.Employee)
            //.Include(x => x.HairService);

            // ??? CUM FAC GROUPBY DUPA EMPLOYEEID, adica sa am (in functie de tabelul din DB): 1 - 1, 2, 6; 2 - 2, 3, 5. Adica ceva de tipul: Key -> List<int>.
            //return context.EmployeesHairServices
            //.GroupBy(obj => obj.EmployeeId);

            //return context.EmployeesHairServices
            //.GroupBy(obj => obj.EmployeeId,
            //(key, list) => new EmployeeHairService { EmployeeId = key, HairServiceId = list.ToList() } );
            //(key, list) => new { EmployeeId = key, HairServiceId = list.ToList() });

            //throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return context.Employees.First(obj => obj.Id == employeeId);
        }

        public async Task<IQueryable<Employee>> GetAllEmployeesByServicesAsync(List<int> servicesIds)
        {
            //var validEmployees = context.Employees.Include(employee => employee.EmployeeHairServices)
            //    .Select(employee => employee.EmployeeHairServices.Where(hairService => servicesIds.Contains(hairService.HairServiceId)));
            //return validEmployees;
            throw new NotImplementedException();
        }

        //public async Task<IQueryable<Employee>> ReadEmployeesAsync()

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
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
