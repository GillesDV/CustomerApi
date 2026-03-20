using CustomerApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();

        public CustomerDto()
        {
        }

        public CustomerDto(int id, string name, string email, List<OrderDto> orders)
        {
            Id = id;
            Name = name;
            Email = email;
            Orders = orders;
        }
    }
}
