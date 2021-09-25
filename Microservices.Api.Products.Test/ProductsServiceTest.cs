using System;
using Xunit;
using MicroserviceCourse.Api.Products.Providers;
using MicroserviceCourse.Api.Products.DB;
using Microsoft.EntityFrameworkCore;
using MicroserviceCourse.Api.Products.Profiles;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq;

namespace Microservices.Api.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductsDbContext(options);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            CreateProducts(dbContext);

            var products = await productsProvider.GetProductsAsync();

            Assert.True(products.isSuccess);
            Assert.True(products.products.Any());
            Assert.Null(products.errorMessage);
        }

        [Fact]
        public async Task GetProductReturnsOneProduct()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductReturnsOneProduct)).Options;
            var dbContext = new ProductsDbContext(options);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            CreateProducts(dbContext);

            var product = await productsProvider.GetProductAsync(11);

            Assert.True(product.isSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.id == 11);
            Assert.Null(product.errorMessage);
        }

        [Fact]
        public async Task GetProductReturnsOneProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductReturnsOneProductUsingInvalidId)).Options;
            var dbContext = new ProductsDbContext(options);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            CreateProducts(dbContext);

            var product = await productsProvider.GetProductAsync(-11);

            Assert.False(product.isSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.errorMessage);
        }
        public void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 0; i < 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    id = i + 10,
                    name = Guid.NewGuid().ToString(),
                    inventory = i + 1,
                    price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
