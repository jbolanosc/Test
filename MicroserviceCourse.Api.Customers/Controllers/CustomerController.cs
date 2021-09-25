using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Customers.Interfaces;
using MicroserviceCourse.Api.Customers.Models;


namespace MicroserviceCourse.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersProvider _provider;
        public CustomerController(ICustomersProvider provider)
        {
            _provider = provider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _provider.GetCustomersAsync();
            if (result.isSuccess)
            {
                return Ok(result.customers);
            }

            return Ok(result.errorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await _provider.GetCustomerAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.customer);
            }

            return NotFound();
        }
    }
}
