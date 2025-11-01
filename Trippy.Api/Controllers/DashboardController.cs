using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IExpenseTypeService _reportService;

        public DashboardController(IExpenseTypeService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("monthly-summary")]
        public async Task<IActionResult> GetMonthlySummary([FromQuery] int year)
        {
            var data = await _reportService.GetMonthlyExpenseInvoiceAsync(year);
            return Ok(data);
        }
    }
}
