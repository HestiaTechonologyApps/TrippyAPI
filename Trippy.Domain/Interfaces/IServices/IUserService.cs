﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;


namespace Trippy.Domain.Interfaces.IServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int id);
        Task<UserDTO> CreateAsync(User coupon);
        Task<bool> UpdateAsync(User coupon);
        Task<bool> DeleteAsync(int id);

    }
}
