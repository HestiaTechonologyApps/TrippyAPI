using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context; // Replace with your DbContext name

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyFinancialDto>> GetMonthlyFinancialAsync(int year)
        {
            // Get monthly expenses
            var monthlyExpenses = await _context.ExpenseMasters
                .Where(e => e.IsActive && !e.IsDeleted && e.CreatedOn.Year == year)
                .GroupBy(e => e.CreatedOn.Month)
                .Select(g => new { Month = g.Key, TotalExpense = g.Sum(e => e.Amount) })
                .ToListAsync();

            // Get monthly invoices
            var monthlyInvoices = await _context.InvoiceMasters
                 .Where(i => !i.IsDeleted && i.CreatedOn.Year == year)
                 .GroupBy(i => i.CreatedOn.Month)
                 .Select(g => new { Month = g.Key, TotalInvoice = g.Sum(i => i.TotalAmount) })
                 .ToListAsync();


            // Combine data for all 12 months
            var result = new List<MonthlyFinancialDto>();
            for (int month = 1; month <= 12; month++)
            {
                var monthName = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
                var expense = monthlyExpenses.FirstOrDefault(e => e.Month == month)?.TotalExpense ?? 0;
                var invoice = monthlyInvoices.FirstOrDefault(i => i.Month == month)?.TotalInvoice ?? 0;

                result.Add(new MonthlyFinancialDto
                {
                    Month = monthName,
                    TotalExpense = expense,
                    TotalInvoice = invoice
                });
            }

            return result;
        }

        public async Task<List<MonthlyTripCountDto>> GetMonthlyTripCountAsync(int year)
        {
            // Get monthly trip counts
            var monthlyTrips = await _context.TripOrders
                .Where(t => t.IsActive && t.FromDate.HasValue && t.FromDate.Value.Year == year)
                .GroupBy(t => t.FromDate.Value.Month)
                .Select(g => new { Month = g.Key, TripCount = g.Count() })
                .ToListAsync();

            // Create result for all 12 months
            var result = new List<MonthlyTripCountDto>();
            for (int month = 1; month <= 12; month++)
            {
                var monthName = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
                var tripCount = monthlyTrips.FirstOrDefault(t => t.Month == month)?.TripCount ?? 0;

                result.Add(new MonthlyTripCountDto
                {
                    Month = monthName,
                    TripCount = tripCount
                });
            }

            return result;
        }

        public async Task<List<VehicleStatusDto>> GetVehicleStatusAsync()
        {
            return await _context.Vehicles
                .GroupBy(v => v.CurrentStatus)
                .Select(g => new VehicleStatusDto
                {
                    StatusName = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(v => v.Count)
                .ToListAsync();
        }

        public async Task<List<ExpenseCategoryDto>> GetExpenseCategoriesAsync()
        {
            return await (from expense in _context.ExpenseMasters
                          join expenseType in _context.ExpenseTypes
                          on expense.ExpenseTypeId equals expenseType.ExpenseTypeId
                          where expense.IsActive && !expense.IsDeleted &&
                                expenseType.IsActive && !expenseType.IsDeleted
                          group expense by expenseType.ExpenseTypeName into g
                          select new ExpenseCategoryDto
                          {
                              CategoryName = g.Key,
                              TotalAmount = g.Sum(e => e.Amount)
                          })
                         .OrderByDescending(e => e.TotalAmount)
                         .Take(5)
                         .ToListAsync();
        }

        public async Task<List<ExpenseCategoryDto>> GetExpenseCategoriesByYearAsync(int year)
        {
            return await (from expense in _context.ExpenseMasters
                          join expenseType in _context.ExpenseTypes
                          on expense.ExpenseTypeId equals expenseType.ExpenseTypeId
                          where expense.IsActive && !expense.IsDeleted &&
                                expenseType.IsActive && !expenseType.IsDeleted &&
                                expense.CreatedOn.Year == year
                          group expense by expenseType.ExpenseTypeName into g
                          select new ExpenseCategoryDto
                          {
                              CategoryName = g.Key,
                              TotalAmount = g.Sum(e => e.Amount)
                          })
                         .OrderByDescending(e => e.TotalAmount)
                         .Take(5)
                         .ToListAsync();
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(int year)
        {
            // Get all data in parallel
            var monthlyFinancialTask = GetMonthlyFinancialAsync(year);
            var monthlyTripCountTask = GetMonthlyTripCountAsync(year);
            var vehicleStatusTask = GetVehicleStatusAsync();
            var expenseCategoriesTask = GetExpenseCategoriesByYearAsync(year);

            await Task.WhenAll(monthlyFinancialTask, monthlyTripCountTask, vehicleStatusTask, expenseCategoriesTask);

            return new DashboardSummaryDto
            {
                MonthlyFinancial = await monthlyFinancialTask,
                MonthlyTripCount = await monthlyTripCountTask,
                VehicleStatus = await vehicleStatusTask,
                ExpenseCategories = await expenseCategoriesTask
            };
        }
    }
}
