using CustomerApi.Application.Interfaces;
using CustomerApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CustomerDb _customerDb;

        public OrderRepository(CustomerDb customerDb)
        {
            _customerDb = customerDb;
        }

        public async Task<Order> AddAsync(Order order)
        {
            var entry = await _customerDb.Orders.AddAsync(order);
            await _customerDb.SaveChangesAsync();

            return entry.Entity;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _customerDb.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _customerDb.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _customerDb.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }
    }
}
