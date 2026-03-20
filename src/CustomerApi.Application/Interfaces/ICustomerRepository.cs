using CustomerApi.Application.DTO;
using CustomerApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Interfaces
{
    public interface ICustomerRepository
    {
            Task<IEnumerable<Customer>> GetAllAsync();
            Task<IEnumerable<Customer>> GetAllWithChildPropsAsync();
            Task<Customer> GetByIdAsync(int id);
            Task<Customer> AddAsync(Customer customer);
            Task DeleteAsync(int id);
    }
}
