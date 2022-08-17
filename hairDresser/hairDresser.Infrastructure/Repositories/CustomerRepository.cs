using hairDresser.Application.Interfaces;
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
        List<Customer> CustomerList = new();

        public CustomerRepository()
        {
            CustomerList.Add(new Customer { Name = "Serb David", Username = "serbdavid", Password = "parola123", Email = "serbdavid@yahoo.com", Phone = "+40763023012", Address = "Timis" });
            CustomerList.Add(new Customer { Name = "Adrian Marin", Username = "adrianmarin", Password = "parola321", Email = "adrianmarin@yahoo.com", Phone = "+40783231930", Address = "Constanta" });
            CustomerList.Add(new Customer { Name = "Vlad Apetrica", Username = "vladapetrica", Password = "parola333", Email = "vladapetrica@yahoo.com", Phone = "+40732012993", Address = "Sighet" });
            CustomerList.Add(new Customer { Name = "Mircea Ghita", Username = "mirceaghita", Password = "333parola", Email = "mirceaghita@yahoo.com", Phone = "+40712023982", Address = "Bucuresti" });
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerAsync(string customerUsername)
        {
            return CustomerList.FirstOrDefault(obj => obj.Username == customerUsername);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return CustomerList;
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
