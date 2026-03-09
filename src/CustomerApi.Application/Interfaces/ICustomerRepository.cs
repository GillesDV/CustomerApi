using CustomerApi.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Interfaces
{
    public interface ICustomerRepository
    {
            Task<IEnumerable<CustomerDto>> GetAllAsync();
            Task<CustomerDto> GetByIdAsync(Guid id);
            Task AddAsync(CustomerDto customer);
            Task DeleteAsync(Guid id);
    }
}
