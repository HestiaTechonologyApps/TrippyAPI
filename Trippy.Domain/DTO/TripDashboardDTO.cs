using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class TripDashboardDTO
    {
        public string Title { get; set; } = "";
        public int Value { get; set; }
        public int Change { get; set; }
        public string Color { get; set; } = "";
        public string Route { get; set; } = "";
        public DateTime? Date { get; set; }
        public string DateString { get; set; } = "";

    }

    public class MonthlyFinancialDto
    {
        public string Month { get; set; } = "";
        public decimal TotalExpense { get; set; }
        public decimal TotalInvoice { get; set; }
    }

    public class MonthlyTripCountDto
    {
        public string Month { get; set; } = "";
        public int TripCount { get; set; }
    }

    public class VehicleStatusDto
    {
        public string StatusName { get; set; } = "";
        public int Count { get; set; }
    }

    public class ExpenseCategoryDto
    {
        public string CategoryName { get; set; } = "";
        public decimal TotalAmount { get; set; }
    }

    public class DashboardSummaryDto
    {
        public List<MonthlyFinancialDto> MonthlyFinancial { get; set; } = new();
        public List<MonthlyTripCountDto> MonthlyTripCount { get; set; } = new();
        public List<VehicleStatusDto> VehicleStatus { get; set; } = new();
        public List<ExpenseCategoryDto> ExpenseCategories { get; set; } = new();
    }
}
