using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer);
        public Customer GetCustomerByCustomerUsername(string customerUsername);
        IEnumerable<Customer> GetCustomers();
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
