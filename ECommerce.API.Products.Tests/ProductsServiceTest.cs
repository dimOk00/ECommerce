using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Profiles;
using ECommerce.API.Products.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.API.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProductsAsync()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProductsAsync))
                .Options;
            
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productService = new ProductService(dbContext, null, mapper);

            var product = await productService.GetProductsAsync();

            Assert.True(product.isSuccess);
            Assert.True(product.products.Any());
            Assert.Null(product.errorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidIdAsync()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidIdAsync))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productService = new ProductService(dbContext, null, mapper);

            var product = await productService.GetProductAsync(1);

            Assert.True(product.isSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.errorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingInvalidIdAsync()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInvalidIdAsync))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productService = new ProductService(dbContext, null, mapper);

            var product = await productService.GetProductAsync(-1);

            Assert.False(product.isSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.errorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }

            dbContext.SaveChanges();
        }
    }
}
