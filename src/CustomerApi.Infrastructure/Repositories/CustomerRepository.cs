using CustomerApi.Application.DTO;
using CustomerApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Infrastructure.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        public Task AddAsync(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
