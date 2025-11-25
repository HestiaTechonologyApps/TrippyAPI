using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace TrippyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Api_PaginatedListDataController : Api_BaseController
    {


        private readonly IListService _service;



        public Api_PaginatedListDataController(IListService service)
        {
            _service = service;
        }

        [HttpGet("trips-paginated")]
        public async Task<CustomApiResponse> GetTripData_Paginated(string ListType, string filtertext="" ,int pagesize = 25,int pagenumber = 0)
        {
            var response = new CustomApiResponse();
            try
            {
                var data = await _service.Get_PaginatedTripList(listType: ListType , pagesize: pagesize, pagenumber: pagenumber, filtertext: filtertext);
                response.IsSucess = true;
                response.Value = data;
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
