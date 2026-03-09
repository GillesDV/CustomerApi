using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Api.Models
{
    public class CreateCustomerRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
