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
            
            return new CustomerDto(result.Id, result.Name, result.Email, null);
        }

        public async Task<CustomerDto> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
            {
                throw new EntityNotFoundException($"Customer with id {id} not found.");
            }

            //TODO use automapper
            return new CustomerDto(customer.Id, customer.Name, customer.Email, null);
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(c => new CustomerDto(c.Id, c.Name, c.Email, null)).ToList();
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
                    var newOrder = DummyDataHelper.GenerateRandomOrder(newCustomer.Id);

                    // should arguably be put in OrderService.cs, but again, we doing quick testing 
                    await _orderRepository.AddAsync(newOrder);
                }

            }            
        }

        public async Task<List<CustomerDto>> GetAllCustomersWithOrdersAsync()
        {
            var customers = await _customerRepository.GetAllWithChildPropsAsync();

            var results = customers
                .Select(c => new CustomerDto(c.Id, c.Name, c.Email, c.Orders.Select(o => new OrderDto(o.Id, o.TotalAmount, c.Id, o.CreatedAt)
                    ).ToList()))
                .ToList();

            return results;

        }

        [Obsolete("This method recreates the n+1 problem. Very inefficient.")]
        public async Task<List<CustomerDto>> GetAllCustomersWithOrdersBadExample()
        {
            var customers = await _customerRepository.GetAllAsync();
            var results = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customer.Id);

                //TODO use automapper
                results.Add(new CustomerDto(customer.Id, customer.Name, customer.Email, orders.Select(o => new OrderDto(o.Id, o.TotalAmount, customer.Id, o.CreatedAt)
                    ).ToList()));
            }

            return results;
        }
    }
}
