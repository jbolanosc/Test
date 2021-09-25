using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Search.Interfaces;
using Microsoft.Extensions.Logging;

namespace MicroserviceCourse.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _http;
        private readonly ILogger<ICustomerService> _logger;

        public CustomerService(IHttpClientFactory http, ILogger<ICustomerService> logger)
        {
            _http = http;
            _logger = logger;
        }
        public async Task<(bool isSuccess, dynamic Customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var client = _http.CreateClient("CustomerService");
                var response = await client.GetAsync($"api/Customer/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content, options);

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
