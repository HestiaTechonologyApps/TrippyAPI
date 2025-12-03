using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDashboardController : ControllerBase
    {
        private readonly ITripOrderService _service;
        private readonly IInvoiceMasterService _invoiceService;
        private readonly ICustomerService _customerService;
        public InvoiceDashboardController(ITripOrderService service, IInvoiceMasterService invoiceService, ICustomerService customerService)
        {
            _service = service;
            _invoiceService = invoiceService;
            _customerService = customerService;
        }
        [HttpGet("GetInvoiceDashboard")]
        public async Task<CustomApiResponse> GetInvoiceDashboardCards(int year)
        {
            var response = new CustomApiResponse(); 
            response.StatusCode = 200;
            try
            {
                var dashboard = await _service.GetAllTripDashboardListbyStatusAsync(year);
                response.IsSucess = true;
                response.Value = dashboard;
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
