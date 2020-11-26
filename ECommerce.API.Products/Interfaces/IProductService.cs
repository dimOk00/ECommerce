using ECommerce.API.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Interfaces
{
    public interface IProductService
    {
        Task<(bool isSuccess, IEnumerable<ProductDto> products, string errorMessage)> GetProductsAsync();
        Task<(bool isSuccess, ProductDto product, string errorMessage)> GetProductAsync(int id);
    }
}
