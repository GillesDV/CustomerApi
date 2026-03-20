using CustomerApi.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomer(int id);
        Task<List<CustomerDto>> GetCustomers();

        Task<CustomerDto> CreateCustomer(CustomerDto customer);

        Task BulkInsertRandomAsync(int count);

        Task<List<CustomerDto>> GetAllCustomersWithOrdersAsync();

        [Obsolete("This method recreates the n+1 problem. Very inefficient.")]
        Task<List<CustomerDto>> GetAllCustomersWithOrdersBadExample();
    }
}
