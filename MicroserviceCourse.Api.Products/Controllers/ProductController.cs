using MicroserviceCourse.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductProvider _provider;
        public ProductController(IProductProvider provider)
        {
            _provider = provider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _provider.GetProductsAsync();
            if (result.isSuccess)
            {
                return Ok(result.products);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _provider.GetProductAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.product);
            }

            return NotFound();
        }
    }
}
