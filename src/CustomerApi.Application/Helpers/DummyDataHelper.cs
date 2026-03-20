using CustomerApi.Application.DTO;
using CustomerApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Infrastructure
{
    public static class DummyDataHelper
    {
        private static readonly Random _random = new();

        public static Customer GenerateRandomCustomer()
        {
            var firstNames = new[] { "Monica", "Erica", "Rita", "Tina", "Sandra", "Mary", "Jessica" };

            var firstName = firstNames[_random.Next(firstNames.Length)];
            var id = _random.Next(int.MaxValue);

            return new Customer()
            {
                Name = firstName,
                Email = $"{firstName.ToLower()}@test.local",
                Id = id
            };
        }

        public static Order GenerateRandomOrder(int? customerId)
        {
            var id = _random.Next(int.MaxValue);

            return new Order()
            {
                Id = id,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = (decimal)(_random.NextDouble() * 1_000),
                CustomerId = customerId ?? 0,
            };
        }

    }
}
