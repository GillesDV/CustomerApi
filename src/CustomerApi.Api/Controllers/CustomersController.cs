using CustomerApi.Api.Models;
using CustomerApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await _customerService.GetCustomers();

            //TODO use automapper
            var results = customers.Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            }).ToList();

            return Ok(results);
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

        [HttpGet]
        [Route("with-orders")]
        public async Task<IActionResult> GetAllCustomersWithOrders()
        {
            var customers = await _customerService.GetAllCustomersWithOrdersAsync();

            // TODO use new DTO
            return Ok(customers);
        }

    }
}
