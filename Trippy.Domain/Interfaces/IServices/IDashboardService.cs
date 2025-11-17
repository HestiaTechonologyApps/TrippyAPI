using Trippy.Domain.DTO;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IDashboardService
    {
        Task<List<MonthlyFinancialDto>> GetMonthlyFinancialAsync(int year);
        Task<List<MonthlyTripCountDto>> GetMonthlyTripCountAsync(int year);
        Task<List<VehicleStatusDto>> GetVehicleStatusAsync();
        Task<List<ExpenseCategoryDto>> GetExpenseCategoriesAsync();
        Task<List<ExpenseCategoryDto>> GetExpenseCategoriesByYearAsync(int year);
        Task<DashboardSummaryDto> GetDashboardSummaryAsync(int year);

    }

    
}
