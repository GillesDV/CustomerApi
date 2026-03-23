using AutoMapper;
using CustomerApi.Application.DTO;
using CustomerApi.Application.Mapping;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Application.UnitTests
{
    public class ApplicationMapperProfileUnitTests
    {
        private readonly IMapper _mapper;

        public ApplicationMapperProfileUnitTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationMappingProfile>());
            configuration.AssertConfigurationIsValid();
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Configuration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationMappingProfile>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void MapCustomerToCustomerDto_MapsScalarPropertiesAndOrders()
        {
            var createdAt = new DateTime(2026, 3, 23, 10, 0, 0, DateTimeKind.Utc);
            var customer = new Customer
            {
                Id = 3,
                Name = "Carol",
                Email = "carol@example.com",
                Orders = new List<Order>
                {
                    new Order { Id = 11, CustomerId = 3, TotalAmount = 19.99m, CreatedAt = createdAt }
                }
            };

            var result = _mapper.Map<CustomerDto>(customer);

            Assert.Equal(3, result.Id);
            Assert.Equal("Carol", result.Name);
            Assert.Equal("carol@example.com", result.Email);
            var order = Assert.Single(result.Orders);
            Assert.Equal(11, order.Id);
            Assert.Equal(3, order.CustomerId);
            Assert.Equal(19.99m, order.TotalAmount);
            Assert.Equal(createdAt, order.CreatedAt);
        }

        [Fact]
        public void MapCustomerDtoToCustomer_MapsScalarPropertiesAndOrders()
        {
            var createdAt = new DateTime(2026, 3, 23, 11, 0, 0, DateTimeKind.Utc);
            var customerDto = new CustomerDto
            {
                Id = 5,
                Name = "Eve",
                Email = "eve@example.com",
                Orders = new List<OrderDto>
                {
                    new OrderDto(15, 25.50m, 5, createdAt)
                }
            };

            var result = _mapper.Map<Customer>(customerDto);

            Assert.Equal(5, result.Id);
            Assert.Equal("Eve", result.Name);
            Assert.Equal("eve@example.com", result.Email);
            var order = Assert.Single(result.Orders);
            Assert.Equal(15, order.Id);
            Assert.Equal(5, order.CustomerId);
            Assert.Equal(25.50m, order.TotalAmount);
            Assert.Equal(createdAt, order.CreatedAt);
        }

        [Fact]
        public void MapOrderToOrderDto_MapsAllProperties()
        {
            var createdAt = new DateTime(2026, 3, 23, 12, 0, 0, DateTimeKind.Utc);
            var order = new Order
            {
                Id = 21,
                CustomerId = 8,
                TotalAmount = 99.95m,
                CreatedAt = createdAt
            };

            var result = _mapper.Map<OrderDto>(order);

            Assert.Equal(21, result.Id);
            Assert.Equal(8, result.CustomerId);
            Assert.Equal(99.95m, result.TotalAmount);
            Assert.Equal(createdAt, result.CreatedAt);
        }
    }
}
