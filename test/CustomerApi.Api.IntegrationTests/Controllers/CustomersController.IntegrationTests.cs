using CustomerApi.Api.Models;
using CustomerApi.Domain.Entities;
using CustomerApi.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Api.IntegrationTests.Controllers
{
    public class CustomersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {

        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public CustomersControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_CustomerExists_When_GetCustomerById_Then_ReturnsOkWithCustomerResponse()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CustomerDb>();

            var customer = new Customer
            {
                Name = "Jack",
                Email = "jack@test.com"
            };

            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            // Act
            var response = await _client.GetAsync($"/api/customers/{customer.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<CustomerResponse>();

            Assert.NotNull(result);
            Assert.Equal(customer.Id, result!.Id);
            Assert.Equal("Jack", result.Name);
            Assert.Equal("jack@test.com", result.Email);
        }

        [Fact]
        public async Task Given_CustomerExists_When_GetCustomerById_Then_ReturnsOkWithCustomerResponse2222()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CustomerDb>();

            // Act
            var response = await _client.GetAsync($"/api/customers/999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<CustomerResponse>();

            Assert.Null(result);
        }

    }
}
