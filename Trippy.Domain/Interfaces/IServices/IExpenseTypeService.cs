using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IExpenseTypeService
    {
        Task<List<ExpenseTypeDTO>> GetAllAsync();
        Task<ExpenseTypeDTO?> GetByIdAsync(int id);
        Task<ExpenseTypeDTO> CreateAsync(ExpenseType coupon);
        Task<bool> UpdateAsync(ExpenseType coupon);
        Task<bool> DeleteAsync(int id);
        Task<List<MonthlyDashboardDTO>> GetMonthlyExpenseInvoiceAsync(int year);

    }
}
