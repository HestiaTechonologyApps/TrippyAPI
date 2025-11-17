using System.Globalization;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;
using Trippy.InfraCore.Data;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _repo; // Replace with your DbContext name

    public DashboardService(IDashboardRepository repository)
    {
        _repo = repository;
    }

    public async Task<List<MonthlyFinancialDto>> GetMonthlyFinancialAsync(int year)
    {
        

        return await _repo.GetMonthlyFinancialAsync(year);
    }

    public async Task<List<MonthlyTripCountDto>> GetMonthlyTripCountAsync(int year)
    {
        return await _repo.GetMonthlyTripCountAsync(year);
    }

    public async Task<List<VehicleStatusDto>> GetVehicleStatusAsync()
    {
        return await _repo.GetVehicleStatusAsync();
    }

    public async Task<List<ExpenseCategoryDto>> GetExpenseCategoriesAsync()
    {
        return await _repo.GetExpenseCategoriesAsync();
    }

    public async Task<List<ExpenseCategoryDto>> GetExpenseCategoriesByYearAsync(int year)
    {
        return await _repo.GetExpenseCategoriesByYearAsync(year);
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
