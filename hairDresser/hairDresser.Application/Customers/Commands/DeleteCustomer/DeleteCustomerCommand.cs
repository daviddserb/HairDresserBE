using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Customer>
    {
        public int Id { get; set; }
    }
}
