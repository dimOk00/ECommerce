﻿using ECommerce.API.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Intefaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, dynamic customer, string errorMessage)> GetCustomersAsync(int customerId);
    }
}
