using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<(bool isSuccess, IEnumerable<Models.Product> products, string errorMessage)> GetProductsAsync();
    }
}
