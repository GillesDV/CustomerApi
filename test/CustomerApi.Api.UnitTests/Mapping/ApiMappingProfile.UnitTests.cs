using AutoMapper;
using CustomerApi.Api.Mapping;
using CustomerApi.Api.Models;
using CustomerApi.Application.DTO;

namespace CustomerApi.Api.UnitTests.Mapping
{
    public class ApiMappingProfileUnitTests
    {
        private readonly IMapper _mapper;

        public ApiMappingProfileUnitTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ApiMappingProfile>());
            configuration.AssertConfigurationIsValid();
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Configuration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ApiMappingProfile>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void MapCreateCustomerRequestToCustomerDto_MapsProperties()
        {
            var request = new CreateCustomerRequest
            {
                Name = "Jane Doe",
                Email = "jane@example.com"
            };

            var result = _mapper.Map<CustomerDto>(request);

            Assert.Equal("Jane Doe", result.Name);
            Assert.Equal("jane@example.com", result.Email);
        }

        [Fact]
        public void MapCustomerDtoToCustomerResponse_MapsProperties()
        {
            var customer = new CustomerDto
            {
                Id = 42,
                Name = "Jane Doe",
                Email = "jane@example.com"
            };

            var result = _mapper.Map<CustomerResponse>(customer);

            Assert.Equal(42, result.Id);
            Assert.Equal("Jane Doe", result.Name);
            Assert.Equal("jane@example.com", result.Email);
        }
    }
}
