using System.Globalization;
using Trippy.Core.Repositories;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;
using Trippy.InfraCore.Data;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _repo; // Replace with your DbContext name

    private readonly ITripOrderRepository _tripOrderRepository;

    private readonly ICurrentUserService _currentUserService;

    public DashboardService(IDashboardRepository repository,ITripOrderRepository tripOrderRepository,ICurrentUserService currentUserService )
    {
        _repo = repository;
        _tripOrderRepository = tripOrderRepository;
        this._currentUserService = currentUserService;
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

    public async Task<List<AuditLogDTO>> GetTripAuxitlogofDay()
    {
        var q= await _tripOrderRepository.GetAuditLogNotifications(int.Parse(_currentUserService.CompanyId), "TripOrders");
        return   q;
    }




    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(int year)
    {
        var monthlyFinancial = await GetMonthlyFinancialAsync(year);
        var monthlyTripCount = await GetMonthlyTripCountAsync(year);
        var vehicleStatus = await GetVehicleStatusAsync();
        var expenseCategories = await GetExpenseCategoriesByYearAsync(year);

        return new DashboardSummaryDto
        {
            MonthlyFinancial = monthlyFinancial,
            MonthlyTripCount = monthlyTripCount,
            VehicleStatus = vehicleStatus,
            ExpenseCategories = expenseCategories
        };
    }
}
