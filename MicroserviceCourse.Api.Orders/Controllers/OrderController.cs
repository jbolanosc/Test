using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Orders.Interfaces;

namespace MicroserviceCourse.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProvider _provider;
        public OrderController(IOrderProvider provider)
        {
            _provider = provider;
        }


        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersAsync(int userId)
        {
            var result = await _provider.GetOrdersAsync(userId);
            if (result.isSuccess)
            {
                return Ok(result.orders);
            }

            return Ok(result.errorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int orderId)
        {
            var result = await _provider.GetOrderAsync(orderId);
            if (result.isSuccess)
            {
                return Ok(result.order);
            }

            return NotFound();
        }
    }
}
