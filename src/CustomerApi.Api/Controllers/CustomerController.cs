using CustomerApi.Api.Models;
using CustomerApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomer(id);

            //TODO use automapper
            var result = new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
            
            return Ok(result);
        }

        [HttpPost]  
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            var customerdto = new Application.DTO.CustomerDto() { Name = request.Name, Email = request.Email };
            var result = await _customerService.CreateCustomer(customerdto);
            
            return CreatedAtAction(nameof(GetCustomerById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Endpoint to fill in the database with dummy customers for testing purposes. 
        /// </summary>
        /// <param name="count">specifies how many customers to create</param>
        [HttpPost("bulk-insert-random/{count:int}")]
        public async Task<IActionResult> InsertRandomCustomers(int count)
        {
            if (count <= 0)
            {
                return BadRequest("Count must be greater than 0.");
            }

            await _customerService.BulkInsertRandomAsync(count);

            return Ok();
        }

    }
}
