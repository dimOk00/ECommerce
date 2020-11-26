using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(ProductsDbContext dbContext, ILogger<ProductService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new Product { Id = 1, Name = "Keyboard", Price = 20, Inventory = 20 });
                _dbContext.Products.Add(new Product { Id = 2, Name = "Mouse", Price = 5, Inventory = 90 });
                _dbContext.Products.Add(new Product { Id = 3, Name = "Monitor", Price = 40, Inventory = 60 });
                _dbContext.Products.Add(new Product { Id = 4, Name = "CPU", Price = 30, Inventory = 140 });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<ProductDto> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool isSuccess, ProductDto product, string errorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product != null)
                {
                    var result = _mapper.Map<Product, ProductDto>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}
