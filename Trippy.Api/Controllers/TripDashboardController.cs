using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trippy.Bussiness.Services;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IServices;
using TrippyAPI.Controllers;

namespace Trippy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripDashboardController : Api_BaseController
    {
        private readonly ITripOrderService _service;
        private readonly IDashboardService _dashboardService;
        private readonly IExpenseTypeService _reportService;
        public TripDashboardController(ITripOrderService service, IExpenseTypeService reportService,IDashboardService dashboardService )
        {
            _service = service;
            _reportService = reportService;
            _dashboardService = dashboardService;
        }
        [HttpGet("GetTripDashboard")]
        public async Task<IActionResult> GetTripDashboard()
        {
            var dashboard = await _service.GetAllTripDashboardListbyStatusAsync();

            return Ok(new
            {
                statusCode = 200,
                isSuccess = true,
                customMessage = "Trip dashboard data retrieved successfully.",
                value = dashboard
            });
        }

        [HttpGet("today")]
        public async Task<ActionResult<List<TripListDataDTO>>> GetTodaysTrips()
        {
            var trips = await _service.GetTodaysTripListAsync();
            return Ok(trips);
        }

        // ✅ 3. Get trips for a specific date
        [HttpGet("by-date")]
        public async Task<ActionResult<List<TripListDataDTO>>> GetTripsByDate([FromQuery] DateTime date)
        {
            var trips = await _service.GetTripsByDateAsync(date);
            return Ok(trips);
        }

        [HttpGet("monthly-summary")]
        public async Task<IActionResult> GetMonthlySummary([FromQuery] int year)
        {
            var data = await _reportService.GetMonthlyExpenseInvoiceAsync(year);
            return Ok(data);
        }


        /// <summary>
        /// Get monthly financial data (expenses vs invoices) for a specific year
        /// </summary>
        [HttpGet("monthly-financial/{year}")]
        public async Task<CustomApiResponse> GetMonthlyFinancial(int year)
        {
            var response = new CustomApiResponse();
            try
            {
                var result = await _dashboardService.GetMonthlyFinancialAsync(year);
                response.IsSucess = true;
                response.Value = result;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Error = ex.Message;
                response.StatusCode = 500;
            }
            return response;
        }

        /// <summary>
        /// Get monthly trip count for a specific year
        /// </summary>
        [HttpGet("monthly-trip-count/{year}")]
        public async Task<CustomApiResponse> GetMonthlyTripCount(int year)
        {
            var response = new CustomApiResponse();
            try
            {
                var result = await _dashboardService.GetMonthlyTripCountAsync(year);
                response.IsSucess = true;
                response.Value = result;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Error = ex.Message;
                response.StatusCode = 500;
            }
            return response;
        }

        /// <summary>
        /// Get current vehicle status distribution
        /// </summary>
        [HttpGet("vehicle-status")]
        public async Task<CustomApiResponse> GetVehicleStatus()
        {
            var response = new CustomApiResponse();
            try
            {
                var result = await _dashboardService.GetVehicleStatusAsync();
                response.IsSucess = true;
                response.Value = result;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Error = ex.Message;
                response.StatusCode = 500;
            }
            return response;
        }

        /// <summary>
        /// Get top expense categories (all time)
        /// </summary>
        [HttpGet("expense-categories")]
        public async Task<CustomApiResponse> GetExpenseCategories()
        {
            var response = new CustomApiResponse();
            try
            {
                var result = await _dashboardService.GetExpenseCategoriesAsync();
                response.IsSucess = true;
                response.Value = result;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Error = ex.Message;
                response.StatusCode = 500;
            }
            return response;
        }

        /// <summary>
        /// Get top expense categories for a specific year
        /// </summary>
        [HttpGet("expense-categories/{year}")]
        public async Task<CustomApiResponse> GetExpenseCategoriesByYear(int year)
        {
            var response = new CustomApiResponse();
            try
            {
                var result = await _dashboardService.GetExpenseCategoriesByYearAsync(year);
                response.IsSucess = true;
                response.Value = result;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Error = ex.Message;
                response.StatusCode = 500;
            }
            return response;
        }

        /// <summary>
        /// Get complete dashboard summary for a specific year
        /// </summary>
        [HttpGet("summary/{year}")]
        public async Task<CustomApiResponse> GetDashboardSummary(int year)
        {
            var response = new CustomApiResponse();
            try
            {
                var result = await _dashboardService.GetDashboardSummaryAsync(year);
                response.IsSucess = true;
                response.Value = result;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Error = ex.Message;
                response.StatusCode = 500;
            }
            return response;
        }
    }
}
