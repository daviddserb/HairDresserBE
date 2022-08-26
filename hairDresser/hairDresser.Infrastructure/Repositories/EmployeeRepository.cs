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
        }

        public async Task<IQueryable<Employee>> ReadEmployeesAsync()
        {
            return context.Employees
                .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
                .ThenInclude(hairServices => hairServices.HairService);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return context.Employees.First(obj => obj.Id == employeeId);
        }

        public async Task<IQueryable<Employee>> GetAllEmployeesByServicesAsync(List<int> servicesIds)
        {
            //???
            //Exemplu de la Adina (dar este eroare):
            //return context.Employees.Include(employee => employee.EmployeeHairServices)
            //    .Select(employee => employee.EmployeeHairServices.Where(hairService => servicesIds.Contains(hairService.HairServiceId)));

            //Ce am incercat sa fac (dar am eroare de can't convert la Contains) Varianta 1:
            //var validEmployees = context.Employees
            //    .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
            //    .ThenInclude(hairServices => hairServices.HairService)
            //    .Where(hairServices => hairServices.EmployeeHairServices.Contains(servicesIds))

            //Ce am incercat sa fac (dar am eroare cand returnez) Varianta 2:
            //var validEmployees = context.Employees
            //    .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
            //    .ThenInclude(hairServices => hairServices.HairService)
            //    .Select(employee => employee.EmployeeHairServices.Where(y => servicesIds.Contains(y.HairServiceId)));

            return context.Employees
                .Include(employeeHairServices => employeeHairServices.EmployeeHairServices)
                .ThenInclude(hairServices => hairServices.HairService);
                //.Where(hairServices => hairServices.EmployeeHairServices.HairServiceId.Contains(servicesIds));
                //.Select(employeeHairServices => employeeHairServices.EmployeeHairServices.Where(hairServicesIds => hairServicesIds.HairServiceId.Contains(servicesIds)));
        }

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
            } catch
            {
                Console.WriteLine("Nu exista employee-ul cu acel id.");
            }
        }
    }
}
