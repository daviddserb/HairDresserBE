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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;

        public CustomerRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
        }

        public async Task<IQueryable<Customer>> ReadCustomersAsync()
        {
            return context.Customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return context.Customers.First(customer => customer.Id == customerId);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
