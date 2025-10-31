using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trippy.Bussiness.Services;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripDashboardController : ControllerBase
    {
        private readonly ITripOrderService _service;

        public TripDashboardController(ITripOrderService service)
        {
            _service = service;
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
        public async Task<ActionResult<List<TripOrderDTO>>> GetTodaysTrips()
        {
            var trips = await _service.GetTodaysTripListAsync();
            return Ok(trips);
        }

        // ✅ 3. Get trips for a specific date
        [HttpGet("by-date")]
        public async Task<ActionResult<List<TripOrderDTO>>> GetTripsByDate([FromQuery] DateTime date)
        {
            var trips = await _service.GetTripsByDateAsync(date);
            return Ok(trips);
        }

    }
}
