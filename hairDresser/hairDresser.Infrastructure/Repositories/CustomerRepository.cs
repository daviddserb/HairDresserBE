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
        }

        public async Task<IQueryable<Customer>> ReadCustomersAsync()
        {
            return context.Customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await context.Customers.FirstOrDefaultAsync(customer => customer.Id == customerId);
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            context.Customers.Update(customer);
            return customer;
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(customer => customer.Id == customerId);
            context.Customers.Remove(customer);
        }
    }
}
