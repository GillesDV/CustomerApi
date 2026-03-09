using CustomerApi.Application.DTO;
using CustomerApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> GetCustomer(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            //TODO use automapper
            return new CustomerDto(customer.Id, customer.Name, customer.Email);
        }
    }
}
