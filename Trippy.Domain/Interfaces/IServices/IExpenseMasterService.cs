using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IExpenseMasterService
    {
        Task<List<ExpenseMarkDTO>> GetAllAsync();
        Task<ExpenseMarkDTO?> GetByIdAsync(int id);
        Task<ExpenseMarkDTO> CreateAsync(ExpenseMaster coupon);
        Task<bool> UpdateAsync(ExpenseMaster coupon);
        Task<bool> DeleteAsync(int id);
        Task <List<ExpenseMasterAuditDTO>>GetExpenseMasterForEntityAsync(string tableName, int recordId);
    }
}
