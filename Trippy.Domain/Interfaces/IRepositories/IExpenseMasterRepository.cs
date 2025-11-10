using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IRepositories
{
    public interface IExpenseMasterRepository : IGenericRepository<ExpenseMaster>
    {
        ExpenseMarkDTO GetExpenseDetails(int expenseId);
        List<ExpenseMarkDTO> GetAllExpenses();
        Task<string?> GenerateExpenseVoucherNumberAsync(ExpenseMaster expenseMaster);
    }
}
