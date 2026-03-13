using CustomerApi.Application.DTO;
using CustomerApi.Application.Exceptions;
using CustomerApi.Application.Interfaces;
using CustomerApi.Domain.Entities;
using CustomerApi.Infrastructure;
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
        private readonly IOrderRepository _orderRepository;

        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<CustomerDto> CreateCustomer(CustomerDto customer)
        {
            //TODO use automapper * 2
            var customerEntity = new CustomerApi.Domain.Entities.Customer
            {
                Name = customer.Name,
                Email = customer.Email
            };

            var result = await _customerRepository.AddAsync(customerEntity);
            
            return new CustomerDto(result.Id, result.Name, result.Email);
        }

        public async Task<CustomerDto> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
            {
                throw new EntityNotFoundException($"Customer with id {id} not found.");
            }

            //TODO use automapper
            return new CustomerDto(customer.Id, customer.Name, customer.Email);
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(c => new CustomerDto(c.Id, c.Name, c.Email)).ToList();
        }

        public async Task BulkInsertRandomAsync(int count)
        {
            var customers = new List<Customer>(count);

            for (int i = 0; i < count; i++)
            {
                var newCustomer = DummyDataHelper.GenerateRandomCustomer();
                await _customerRepository.AddAsync(newCustomer);

                // Give some of them semi-randomly orders (for testing purposes)
                if (newCustomer.Id % 2 == 0) {
                    var newOrder = DummyDataHelper.GenerateRandomOrder(newCustomer);

                    // should arguably be put in OrderService.cs, but again, we doing quick testing 
                    await _orderRepository.AddAsync(newOrder);
                }

            }            
        }

    }
}
