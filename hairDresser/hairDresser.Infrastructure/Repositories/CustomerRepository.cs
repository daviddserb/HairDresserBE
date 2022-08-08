using hairDresser.Domain.Interfaces;
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
        List<Customer> CustomerList = new List<Customer>();

        public CustomerRepository()
        {
            CustomerList.Add(new Customer("Serb David", "serbdavid", "parola123", "serbdavid@yahoo.com", "+40763023012", "Timis"));
            CustomerList.Add(new Customer("Adrian Marin", "adrianmarin", "parola321", "adrianmarin@yahoo.com", "+40783231930", "Constanta"));
            CustomerList.Add(new Customer("Vlad Apetrica", "vladapetrica", "parola333", "vladapetrica@yahoo.com", "+40732012993", "Sighet"));
            CustomerList.Add(new Customer("Mircea Ghita", "mirceaghita", "333parola", "mirceaghita@yahoo.com", "+40712023982", "Bucuresti"));
        }

        public void CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByCustomerName(string customerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return CustomerList;
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
