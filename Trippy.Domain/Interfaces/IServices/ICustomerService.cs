﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.DTO;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllAsync();
        Task<CustomerDTO?> GetByIdAsync(int id);
        Task<CustomerDTO> CreateAsync(Customer coupon);
        Task<bool> UpdateAsync(Customer coupon);
        Task<bool> DeleteAsync(int id);
    }
}
