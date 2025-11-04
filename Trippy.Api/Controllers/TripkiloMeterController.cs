using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trippy.Bussiness.Services;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripkiloMeterController : ControllerBase
    {
        private readonly ITripKiloMeterService _service;
        public TripkiloMeterController(ITripKiloMeterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<CustomApiResponse> GetAll()
        {
            var response = new CustomApiResponse();
            try
            {
                var tripkiloMeters = await _service.GetAllAsync();
                response.IsSucess = true;
                response.Value = tripkiloMeters;
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

        [HttpGet("{id}")]
        public async Task<CustomApiResponse> GetById(int id)
        {
            var response = new CustomApiResponse();
            var tripKiloMeter = await _service.GetByIdAsync(id);
            if (tripKiloMeter == null)
            {
                response.IsSucess = false;
                response.Error = "Not found";
                response.StatusCode = 404;
            }
            else
            {
                response.IsSucess = true;
                response.Value = tripKiloMeter;
                response.StatusCode = 200;
            }
            return response;
        }
        [HttpPost]
        public async Task<CustomApiResponse> Create([FromBody] TripKiloMeter tripkiloMeter)
        {
            var response = new CustomApiResponse();
            try
            {
                var created = await _service.CreateAsync(tripkiloMeter);
                response.IsSucess = true;
                response.Value = created;
                response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Error = ex.Message;
                response.StatusCode = 500;
            }
            return response;
        }

        [HttpPut("{id}")]
        public async Task<CustomApiResponse> Update(int id, [FromBody] TripKiloMeter tripKiloMeter)
        {
            var response = new CustomApiResponse();
            if (id != tripKiloMeter.TripKiloMeterId)
            {
                response.IsSucess = false;
                response.Error = "Id mismatch";
                response.StatusCode = 400;
                return response;
            }
            var updated = await _service.UpdateAsync(tripKiloMeter);
            if (!updated)
            {
                response.IsSucess = false;
                response.Error = "Not found";
                response.StatusCode = 404;
            }
            else
            {
                response.IsSucess = true;
                response.Value = tripKiloMeter;
                response.StatusCode = 200;
            }
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<CustomApiResponse> Delete(int id)
        {
            var response = new CustomApiResponse();
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                response.IsSucess = false;
                response.Error = "Not found";
                response.StatusCode = 404;
            }
            else
            {
                response.IsSucess = true;
                response.Value = null;
                response.StatusCode = 204;
            }
            return response;
        }

        
    }
}
