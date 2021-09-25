using MicroserviceCourse.Api.Products.Interfaces;
using MicroserviceCourse.Api.Products.Models;
using MicroserviceCourse.Api.Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Api.Products.Providers
{
    public class ProductsProvider : IProductProvider
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public ProductsProvider(ProductsDbContext context, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            seedData();
        }

        public async Task<(bool isSuccess, Models.Product product, string errorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product != null)
                {
                    var result = _mapper.Map<DB.Product, Models.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Product> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();

                if(products != null || products.Any())
                {
                   var result = _mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
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
            _context.Database.EnsureDeleted();
            if (!_context.Products.Any())
            {
                _context.Products.Add(new DB.Product() { id = 1, name = "PS4", price = 299, inventory = 10 });
                _context.Products.Add(new DB.Product() { id = 2, name = "PS5", price = 499, inventory = 2 });
                _context.Products.Add(new DB.Product() { id = 3, name = "PC", price = 2000, inventory = 10 });
                _context.SaveChanges();
            }
        }
    }
}
