using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.DTO
{
    public class CreateCustomerDto
    {
        public string Name { get; set; } 
        public string Email { get; set; }
    }
}
