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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerDb.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllWithChildPropsAsync()
        {
            return await _customerDb.Customers.Include(c => c.Orders).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _customerDb.Customers.FindAsync(id);
        }
    }
}
