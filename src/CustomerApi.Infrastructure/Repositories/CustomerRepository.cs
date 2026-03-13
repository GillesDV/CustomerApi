using CustomerApi.Application.DTO;
using CustomerApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Infrastructure.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDb _customerDb;

        public CustomerRepository(CustomerDb customerDb)
        {
            _customerDb = customerDb;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            var entry = await _customerDb.Customers.AddAsync(customer);    
            await _customerDb.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task BulkInsertRandomAsync(int count)
        {
            var customers = new List<Customer>(count);

            for (int i = 0; i < count; i++)
            {
                customers.Add(DummyDataHelper.GenerateRandomCustomer());
            }

            await _customerDb.Customers.AddRangeAsync(customers);
            await _customerDb.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerDb.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _customerDb.Customers.FindAsync(id);
        }
    }
}
