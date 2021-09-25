using MicroserviceCourse.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Search.Interfaces;

namespace MicroserviceCourse.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
            private readonly IHttpClientFactory _http;
        public OrderService(IHttpClientFactory http, ILogger<OrderService> logger)
        {
            _logger = logger;
            _http = http;
        }
        public async Task<(bool isSuccess, IEnumerable<Order> orders, string errorMsg)> GetOrderAsync(int customerId)
        {
            try
            {
                var client = _http.CreateClient("OrderService");
                var response = await client.GetAsync($"api/Order/user/{customerId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

                    return (true, result, "");
                }

                return (false, null, "No results");
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}
