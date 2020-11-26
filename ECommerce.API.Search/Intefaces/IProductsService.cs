using ECommerce.API.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Intefaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync();
    }
}
