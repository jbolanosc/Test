using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Orders.Models;

namespace MicroserviceCourse.Api.Orders.Interfaces
{
    public interface IOrderItemProvider
    {
        Task<(bool isSuccess, IEnumerable<OrderItem> orderItems, string errorMessage)> GetOrderItemsAsync(int orderId);
        Task<(bool isSuccess, OrderItem orderItem, string errorMessage)> GetOrderItemAsync(int id);
    }
}
