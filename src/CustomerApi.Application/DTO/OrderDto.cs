using CustomerApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
