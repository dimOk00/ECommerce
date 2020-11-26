using AutoMapper;
using ECommerce.API.Customers.DB;
using ECommerce.API.Customers.Models;

namespace ECommerce.API.Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
