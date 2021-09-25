using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMsg)>GetOrderAsync(int customerId);
    }
}
