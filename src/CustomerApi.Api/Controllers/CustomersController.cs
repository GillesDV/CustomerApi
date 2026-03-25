using AutoMapper;
using CustomerApi.Api.Models;
using CustomerApi.Api.Validation;
using CustomerApi.Application.DTO;
using CustomerApi.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IValidator<CreateCustomerRequest> _createValidator;

        public CustomersController(ILogger<CustomersController> logger, IMapper mapper, ICustomerService customerService, IValidator<CreateCustomerRequest> validator)
        {
            _logger = logger;
            _mapper = mapper;
            _customerService = customerService;
            _createValidator = validator;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomer(id);

            if (customer is null)
            {
                return NotFound();
            }

            var result = _mapper.Map<CustomerResponse>(customer);

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await _customerService.GetCustomers();
            var results = _mapper.Map<List<CustomerResponse>>(customers);

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            //Could also move this into a Filter or Mediatr pattern, but simplicity wins. Also FluentValidation.AspNetCore is deprecated, so no dice
            var resultValidation = await _createValidator.ValidateAsync(request);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var customerDto = _mapper.Map<CustomerDto>(request);
            var result = await _customerService.CreateCustomer(customerDto);
            var response = _mapper.Map<CustomerResponse>(result);

            return CreatedAtAction(nameof(GetCustomerById), new { id = response.Id }, response);
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
