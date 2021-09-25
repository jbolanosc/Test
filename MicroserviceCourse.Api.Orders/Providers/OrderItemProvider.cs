using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MicroserviceCourse.Api.Orders.Interfaces;
using MicroserviceCourse.Api.Orders.Providers;
using MicroserviceCourse.Api.Orders.Db;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace MicroserviceCourse.Api.Orders.Providers
{
    public class OrderItemProvider : IOrderItemProvider
    {
        private readonly OrdersDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OrderItemProvider(OrdersDbContext context, ILogger<OrderItemProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            seedData();
        }
        public async Task<(bool isSuccess, Models.OrderItem orderItem, string errorMessage)> GetOrderItemAsync(int id)
        {
            try
            {
                var orderItem = await _context.orderItems.FindAsync(id);

                if (orderItem != null)
                {
                    var result = _mapper.Map<Db.OrderItem, Models.OrderItem>(orderItem);
                    return (true, result, "");
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.OrderItem> orderItems, string errorMessage)> GetOrderItemsAsync(int orderId)
        {
            try
            {
                var orderItems = await _context.orderItems.Where(o => o.orderId == orderId).ToListAsync();

                if (orderItems != null || orderItems.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.OrderItem>, IEnumerable<Models.OrderItem>>(orderItems);
                    return (true, result, "");
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }


        public void seedData()
        {
            if (!_context.orderItems.Any())
            {
                _context.Add(new Db.OrderItem() { id = 1, orderId = 1, productId = 1, quantity = 10, unitPrice = 10});
                _context.Add(new Db.OrderItem() { id = 2, orderId = 2, productId = 2, quantity = 10, unitPrice = 110 });
                _context.Add(new Db.OrderItem() { id = 3, orderId = 3, productId = 13, quantity = 10, unitPrice = 120 });
                _context.SaveChanges();
            }
        }
    }
}
