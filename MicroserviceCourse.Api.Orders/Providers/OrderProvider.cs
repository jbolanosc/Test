using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MicroserviceCourse.Api.Orders.Db;
using Microsoft.Extensions.Logging;
using MicroserviceCourse.Api.Orders.Interfaces;

namespace MicroserviceCourse.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrdersDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OrderProvider(OrdersDbContext context, ILogger<OrderProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            seedData();
        }
        public async Task<(bool isSuccess, Models.Order order, string errorMessage)> GetOrderAsync(int orderId)
        {
            try
            {
                var order = await _context.orders.FindAsync(orderId);

                if (order != null)
                {
                    var result = _mapper.Map<Db.Order, Models.Order>(order);
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

        public async Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrdersAsync(int userId)
        {
            try
            {
                var orders = await _context.orders.Where(o => o.customerId == userId).ToListAsync();

                if (orders != null || orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
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

        public void getTotal(Order order)
        {
            var result = order.items.Sum(i => i.quantity * i.unitPrice);
            order.total = result;
        }


        public void seedData()
        {
            if (!_context.orders.Any())
            {
                _context.Add(new Db.Order() { id = 1, customerId = 1, orderDate = DateTime.Now });
                _context.Add(new Db.Order() { id = 2, customerId = 2, orderDate = DateTime.Now });
                _context.Add(new Db.Order() { id = 3, customerId = 1, orderDate = DateTime.Now });
                _context.SaveChanges();
            }
        }
    }
}
