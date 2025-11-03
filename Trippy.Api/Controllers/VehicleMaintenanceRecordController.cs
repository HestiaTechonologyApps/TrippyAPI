using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMaintenanceRecordController : ControllerBase
    {
        private readonly IVehicleMaintenanceRecordService _service;
        public VehicleMaintenanceRecordController(IVehicleMaintenanceRecordService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<CustomApiResponse> GetAll()
        {
            var response = new CustomApiResponse();
            try
            {
                var vehicleMaintenanceRecords = await _service.GetAllAsync();
                response.IsSucess = true;
                response.Value = vehicleMaintenanceRecords;
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
            var vehicleMaintenanceRecord = await _service.GetByIdAsync(id);
            if (vehicleMaintenanceRecord == null)
            {
                response.IsSucess = false;
                response.Error = "Not found";
                response.StatusCode = 404;
            }
            else
            {
                response.IsSucess = true;
                response.Value = vehicleMaintenanceRecord;
                response.StatusCode = 200;
            }
            return response;
        }
        [HttpPost]
        public async Task<CustomApiResponse> Create([FromBody] VehicleMaintenanceRecord vehicleMaintenanceRecord)
        {
            var response = new CustomApiResponse();
            try
            {
                var created = await _service.CreateAsync(vehicleMaintenanceRecord);
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
        public async Task<CustomApiResponse> Update(int id, [FromBody] VehicleMaintenanceRecord vehicleMaintenance)
        {
            var response = new CustomApiResponse();
            if (id != vehicleMaintenance.VehicleMaintenanceRecordId)
            {
                response.IsSucess = false;
                response.Error = "Id mismatch";
                response.StatusCode = 400;
                return response;
            }
            var updated = await _service.UpdateAsync(vehicleMaintenance);
            if (!updated)
            {
                response.IsSucess = false;
                response.Error = "Not found";
                response.StatusCode = 404;
            }
            else
            {
                response.IsSucess = true;
                response.Value = vehicleMaintenance;
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
