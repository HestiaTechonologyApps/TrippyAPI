using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripOrderController : ControllerBase
    {
        private readonly ITripOrderService _service;
        private readonly ITripNoteService _tripNotesService;
        public TripOrderController(ITripOrderService service ,ITripNoteService tripNotesService)
        {
            _service = service;
            _tripNotesService = tripNotesService;
        }
        [HttpGet("GetAllTripLists")]
        public async Task<CustomApiResponse> GetAllTripList([FromQuery] string? status = null, [FromQuery] int? year = null, [FromQuery] int userId = 0)
        {
            var response = new CustomApiResponse();

            try
            {
                var tripOrders = await _service.GetAllTripListAsync(status, year);
                response.IsSucess = true;
                response.Value = tripOrders;
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

        [HttpGet("GetAllTripList")]
        public async Task<CustomApiResponse> GetAll()
        {
            var response = new CustomApiResponse();
            try
            {
                var tripOrders = await _service.GetAll();
                response.IsSucess = true;
                response.Value = tripOrders;
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

        [HttpGet("GetAllTripListbyStatus")]
        public async Task <CustomApiResponse> GetAllTripListbyStatus(String Status,int Userid=0)
        {
            var response = new CustomApiResponse();
            try
            {
                var tripOrders = await _service.GetAllTripListbyStatusAsync(Status);
                response.IsSucess = true;
                response.Value = tripOrders;
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
        [HttpGet]
        public async Task<CustomApiResponse> GetAllAsync()
        {
            var response = new CustomApiResponse();
            try
            {
                var tripOrders = await _service.GetAllAsync();
                response.IsSucess = true;
                response.Value = tripOrders;
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
            var tripOrder = await _service.GetByIdAsync(id);
            if (tripOrder == null)
            {
                response.IsSucess = false;
                response.Error = "Not found";
                response.StatusCode = 404;
            }
            else
            {
                response.IsSucess = true;
                response.Value = tripOrder;
                response.StatusCode = 200;
            }
            return response;
        }

        [HttpPost]
        public async Task<CustomApiResponse> Create([FromBody] TripOrder tripOrder)
        {
            var response = new CustomApiResponse();
            try
            {
                var created = await _service.CreateAsync(tripOrder);
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
        public async Task<CustomApiResponse> Update(int id, [FromBody] TripOrder tripOrder)
        {
            var response = new CustomApiResponse();
            if (id != tripOrder.TripOrderId)
            {
                response.IsSucess = false;
                response.Error = "Id mismatch";
                response.StatusCode = 400;
                return response;
            }
            var updated = await _service.UpdateAsync(tripOrder);
            if (!updated)
            {
                response.IsSucess = false;
                response.Error = "Not found";
                response.StatusCode = 404;
            }
            else
            {
                response.IsSucess = true;
                response.Value = tripOrder;
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
        [HttpGet("GetTripNotes")]
        public async Task <CustomApiResponse> GetTripNotes()
        {
            var response = new CustomApiResponse();
            try
            {
                var tripNotes = await _tripNotesService.GetAllAsync();
                response.IsSucess = true;
                response.Value = tripNotes;
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

        [HttpGet("CancelTrip")]
        public async Task <CustomApiResponse> CancelTrip()
        {
            var response = new CustomApiResponse();
            try
            {
                var canceltrip = await _service.GetCanceledTripsAsync();
                response.IsSucess = true;
                response.Value = canceltrip;
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



        [HttpGet("GetTripNotespfTrip")]
        public async Task <CustomApiResponse> GetTripNotesOfTrip(int id)
        {
            var response = new CustomApiResponse();
            try
            {
                var tripNotes = await _tripNotesService.GetTripNotesOfTrip(id);
                response.IsSucess = true;
                response.Value = tripNotes;
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


        [HttpPost("CreateTripNotes")]
        public async Task<CustomApiResponse> CreateTripNotes([FromBody] TripNotes tripNotes)
        {
            var response = new CustomApiResponse();
            try
            {
                var created = await _tripNotesService.CreateAsync(tripNotes);
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


    }
}