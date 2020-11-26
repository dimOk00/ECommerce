using ECommerce.API.Orders.DB;
using ECommerce.API.Orders.Models;

namespace ECommerce.API.Orders.Services
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
