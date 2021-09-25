using MicroserviceCourse.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Products.Interfaces
{
    public interface IProductProvider
    {
        Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync();
        Task<(bool isSuccess, Product product, string errorMessage)> GetProductAsync(int id);

    }
}
