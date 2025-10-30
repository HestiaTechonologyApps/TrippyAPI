using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trippy.Bussiness.Services;
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
      
    }
}
