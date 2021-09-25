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
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemProvider _provider;
        public OrderItemController(IOrderItemProvider provider)
        {
            _provider = provider;
        }
        [HttpGet("order/{userId}")]
        public async Task<IActionResult> GetOrderItemsAsync(int orderId)
        {
            var result = await _provider.GetOrderItemsAsync(orderId);
            if (result.isSuccess)
            {
                return Ok(result.orderItems);
            }

            return Ok(result.errorMessage);
        }

        [HttpGet("{orderItemId}")]
        public async Task<IActionResult> GetOrderItemAsync(int orderItemId)
        {
            var result = await _provider.GetOrderItemAsync(orderItemId);
            if (result.isSuccess)
            {
                return Ok(result.orderItem);
            }

            return NotFound();
        }
    }
}
