using MicroserviceCourse.Api.Search.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Search.Services {
    public class SearchService : ISearchService
    {
        private readonly IOrderService _service;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public SearchService(IOrderService service, IProductService productService, ICustomerService customerService)
        {
            _service = service;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<(bool isSuccess, dynamic searchResults)> SearchAsync(int customerId)
        {
            var orderResult = await _service.GetOrderAsync(customerId);
            var productResult = await _productService.GetProductsAsync();
            var customerResult = await _customerService.GetCustomerAsync(customerId);

            if (orderResult.isSuccess)
            {
                foreach (var order in orderResult.orders) 
                {
                    foreach(var item in order.items)
                    {
                        item.productName = productResult.isSuccess ? productResult.products.FirstOrDefault(p => p.id == item.productId)?.name : "Product info not available";
                    }
                }

                var result = new
                {
                    Customer = customerResult.isSuccess ? customerResult.Customer : "Information not available",
                    Orders = orderResult.orders
                };

                return (true, result);
            }
            return (false, null);
        }
    }
}
