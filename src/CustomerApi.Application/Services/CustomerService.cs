using AutoMapper;
using CustomerApi.Application.DTO;
using CustomerApi.Application.Interfaces;
using CustomerApi.Domain.Entities;
using CustomerApi.Infrastructure;

namespace CustomerApi.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerService(IMapper mapper, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<CustomerDto> CreateCustomer(CustomerDto customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            var result = await _customerRepository.AddAsync(customerEntity);

            return _mapper.Map<CustomerDto>(result);
        }

        public async Task<CustomerDto?> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
            {
                return null;
            }

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();

            return _mapper.Map<List<CustomerDto>>(customers);
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

            return _mapper.Map<List<CustomerDto>>(customers);
        }

        [Obsolete("This method recreates the n+1 problem. Very inefficient.")]
        public async Task<List<CustomerDto>> GetAllCustomersWithOrdersBadExample()
        {
            var customers = await _customerRepository.GetAllAsync();
            var results = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customer.Id);
                var customerDto = _mapper.Map<CustomerDto>(customer);
                customerDto.Orders = _mapper.Map<List<OrderDto>>(orders);
                results.Add(customerDto);
            }

            return results;
        }
    }
}
