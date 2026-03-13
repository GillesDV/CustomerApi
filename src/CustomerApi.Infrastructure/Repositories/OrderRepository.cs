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

        public Task<Order> AddAsync(Order order)
        {
            throw new NotImplementedException();
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
    }
}
