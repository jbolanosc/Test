using MicroserviceCourse.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using MicroserviceCourse.Api.Search.Interfaces;
using System.Text.Json;

namespace MicroserviceCourse.Api.Search.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _http;
        private readonly ILogger<IProductService> _logger;

        public ProductService( IHttpClientFactory http, ILogger<IProductService> logger)
        {
            _http = http;
            _logger = logger;
        }
        public async Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var client = _http.CreateClient("ProductService");
                var response = await client.GetAsync("api/Product");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
