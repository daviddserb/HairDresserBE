using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.Id);

            if (customer == null) return null;

            customer.Name = request.Name;
            customer.Username = request.Username;
            customer.Password = request.Password;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.Address = request.Address;

            await _unitOfWork.CustomerRepository.UpdateCustomerAsync(customer);
            await _unitOfWork.SaveAsync();

            return customer;
        }
    }
}
