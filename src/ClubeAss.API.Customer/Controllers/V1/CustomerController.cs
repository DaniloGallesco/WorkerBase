using ClubeAss.API.Customer.ViewModel.Customer;
using ClubeAss.Domain.Commands;
using ClubeAss.Domain.Interface.Application;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ClubeAss.API.Customer.Controllers.V1
{
    [Route("API/V1/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;
        private readonly IValidator<CustomerAddRequest> _validatorCustomerAddRequest;
        private readonly IValidator<CustomerDeleteRequest> _validatorCustomerDeleteRequestValidator;
        private readonly IValidator<CustomerGetRequest> _validatorCustomerGetRequestValidator;
        private readonly IValidator<CustomerUpdateRequest> _validatorCustomerUpdateRequestValidator;
        private readonly IMemoryCache _cache;



        public CustomerController(ICustomerApplication customerApplication,
            IValidator<CustomerAddRequest> validatorCustomerAddRequest,
            IValidator<CustomerDeleteRequest> validatorCustomerDeleteRequestValidator,
            IValidator<CustomerGetRequest> validatorCustomerGetRequestValidator,
            IValidator<CustomerUpdateRequest> validatorCustomerUpdateRequestValidator,
            IMemoryCache memoryCache
            )
        {
            _customerApplication = customerApplication;
            _validatorCustomerAddRequest = validatorCustomerAddRequest;
            _validatorCustomerDeleteRequestValidator = validatorCustomerDeleteRequestValidator;
            _validatorCustomerGetRequestValidator = validatorCustomerGetRequestValidator;
            _validatorCustomerUpdateRequestValidator = validatorCustomerUpdateRequestValidator;
            _cache = memoryCache;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            BaseResponse response;

            if (!_cache.TryGetValue("Customer.List", out response))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(999));

                response = await _customerApplication.GetAll();

                _cache.Set("Customer.List", response, cacheEntryOptions);
            }

            return new ObjectResult(response) { StatusCode = response.StatusCode.GetHashCode() };
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerAddRequest cliente)
        {
            var _validator = await _validatorCustomerAddRequest.ValidateAsync(cliente);

            if (!_validator.IsValid)
            {
                var validador = Extensions.Fluentvalidation.FluentValidation.Errors(_validator.Errors).Result;
                return new ObjectResult(validador) { StatusCode = validador.StatusCode.GetHashCode() };
            }

            var response = await _customerApplication.Add(cliente);

            _cache.Remove("Customer.List");

            return StatusCode(response.StatusCode.GetHashCode());
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {

            var request = new CustomerGetRequest(id);
            var _validator = await _validatorCustomerGetRequestValidator.ValidateAsync(request);

            if (!_validator.IsValid)
            {
                var validador = Extensions.Fluentvalidation.FluentValidation.Errors(_validator.Errors).Result;
                return new ObjectResult(validador) { StatusCode = validador.StatusCode.GetHashCode() };
            }

            BaseResponse response;
            if (!_cache.TryGetValue("Customer.Get." + id.ToString(), out response))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(999));


                response = await _customerApplication.GetByid(request);

                _cache.Set("Customer.Get." + id.ToString(), response, cacheEntryOptions);
            }


            return new ObjectResult(response) { StatusCode = response.StatusCode.GetHashCode() };
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CustomerUpdateRequest request)
        {

            var _validator = await _validatorCustomerUpdateRequestValidator.ValidateAsync(request);

            if (!_validator.IsValid)
            {
                var validador = Extensions.Fluentvalidation.FluentValidation.Errors(_validator.Errors).Result;
                return new ObjectResult(validador) { StatusCode = validador.StatusCode.GetHashCode() };
            }

            var response = await _customerApplication.Update(id, request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response) { StatusCode = response.StatusCode.GetHashCode() };
            }
            _cache.Remove("Customer.List");
            _cache.Remove("Customer.Get." + id.ToString());
            return StatusCode(response.StatusCode.GetHashCode());
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new CustomerDeleteRequest(id);
            var _validator = await _validatorCustomerDeleteRequestValidator.ValidateAsync(request);

            if (!_validator.IsValid)
            {
                var validador = Extensions.Fluentvalidation.FluentValidation.Errors(_validator.Errors).Result;
                return new ObjectResult(validador) { StatusCode = validador.StatusCode.GetHashCode() };
            }

            var response = await _customerApplication.Remove(new CustomerDeleteRequest(id));

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response) { StatusCode = response.StatusCode.GetHashCode() };
            }

            _cache.Remove("Customer.List");
            _cache.Remove("Customer.Get." + id.ToString());
            return StatusCode(response.StatusCode.GetHashCode());
        }

    }
}