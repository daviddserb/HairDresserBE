using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address
            };
            await _unitOfWork.CustomerRepository.CreateCustomerAsync(customer);
            await _unitOfWork.SaveAsync();
            return customer;
        }
    }
}
