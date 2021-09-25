using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Orders.Models;

namespace MicroserviceCourse.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool isSuccess, IEnumerable<Order> orders, string errorMessage)> GetOrdersAsync(int userId);
        Task<(bool isSuccess, Order order, string errorMessage)> GetOrderAsync(int id);
    }
}
