using hairDresser.Domain.Models;
using hairDresser.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Employees.Queries.GetEmployeesByServices
{
    public class GetEmployeesByServicesQueryHandler : IRequestHandler<GetEmployeesByServicesQuery, IEnumerable<Employee>>
    {
        private IEmployeeRepository _employeeRepository;
        private IHairServiceRepository _hairServiceRepository;

        public GetEmployeesByServicesQueryHandler(IEmployeeRepository employeeRepository, IHairServiceRepository hairServiceRepository)
        {
            _employeeRepository = employeeRepository;
            _hairServiceRepository = hairServiceRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetEmployeesByServicesQuery request, CancellationToken cancellationToken)
        {
            var hairServicesPickedByCustomer = await _hairServiceRepository.GetHairServiceAsync(request.HairServicesId);
            var hairServices = "";
            foreach (var service in hairServicesPickedByCustomer)
            {
                hairServices += service.Name;
                if (service != hairServicesPickedByCustomer.Last())
                {
                    hairServices += ", ";
                }
            }

            var validEmployees = await _employeeRepository.GetEmployeesAsync(hairServices);

            return await Task.FromResult(validEmployees);
        }
    }
}
