using ECommerce.API.Search.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _orderService;
        private readonly IProductsService _productsService;
        private readonly ICustomersService _customersService;

        public SearchService(IOrdersService orderService, IProductsService productsService, ICustomersService customersService)
        {
            _orderService = orderService;
            this._productsService = productsService;
            this._customersService = customersService;
        }

        public async Task<(bool isSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await _orderService.GetOrdersAsync(customerId);
            var productsResult = await _productsService.GetProductsAsync();
            var cutomersResult = await _customersService.GetCustomersAsync(customerId);

            if (ordersResult.isSuccess)
            {
                foreach (var order in ordersResult.orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess 
                            ? productsResult.products.FirstOrDefault(x => x.Id == item.ProductId)?.Name
                            : "Product info is not available";
                    }
                }

                var result = new
                {
                    Customer = cutomersResult.IsSuccess
                            ? cutomersResult.customer 
                            : new { Name = "Customer info is not available" },

                    Orders = ordersResult.orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
