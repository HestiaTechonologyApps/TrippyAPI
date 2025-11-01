using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class ExpenseTypeRepository : GenericRepository<ExpenseType>, IExpenseTypeRepository
    {
        private readonly AppDbContext _context;
        public ExpenseTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MonthlyDashboardDTO>> GetMonthlyExpenseInvoiceAsync(int year)
        {
            // 1️⃣ Aggregate expenses by month
            var expenses = await _context.ExpenseMasters
                .Where(e => e.CreatedOn.Year == year && !e.IsDeleted)
                .GroupBy(e => e.CreatedOn.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalExpense = g.Sum(x => x.ExpenseTypeId) // replace with actual expense amount if you have one
                })
                .ToListAsync();

            // 2️⃣ Aggregate invoices by month
            var invoices = await _context.InvoiceMasters
                .Where(i => i.CreatedOn.Year == year && !i.IsDeleted)
                .GroupBy(i => i.CreatedOn.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalInvoice = g.Sum(x => x.TotalAmount)
                })
                .ToListAsync();

            // 3️⃣ Merge both lists
            var allMonths = Enumerable.Range(1, 12)
                .Select(m => new MonthlyDashboardDTO
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m),
                    Expense = expenses.FirstOrDefault(e => e.Month == m)?.TotalExpense ?? 0,
                    Invoice = invoices.FirstOrDefault(i => i.Month == m)?.TotalInvoice ?? 0
                })
                .ToList();

            return allMonths;
        }
    }
}
