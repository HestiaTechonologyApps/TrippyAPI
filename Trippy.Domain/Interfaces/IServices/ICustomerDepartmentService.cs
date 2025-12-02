using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ICustomerDepartmentService
    {
        Task<List<CustomerDepartmentDTO>> GetAllAsync();
        Task<CustomerDepartmentDTO?> GetByIdAsync(int id);
        Task<CustomerDepartmentDTO> CreateAsync(CustomerDepartment coupon);
        Task<bool> UpdateAsync(CustomerDepartment coupon);
        Task<bool> DeleteAsync(int id);
    }
}
