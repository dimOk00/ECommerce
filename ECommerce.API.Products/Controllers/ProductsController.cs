using ECommerce.API.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsnc()
        {
            var (isSuccess, products, errorMessage) = await _productService.GetProductsAsync();
            if (isSuccess)
            {
                return Ok(products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var (isSuccess, products, errorMessage) = await _productService.GetProductAsync(id);
            if (isSuccess)
            {
                return Ok(products);
            }
            return NotFound();
        }
    }
}
