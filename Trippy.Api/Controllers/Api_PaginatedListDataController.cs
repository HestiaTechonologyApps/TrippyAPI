using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace TrippyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class Api_PaginatedListDataController : Api_BaseController
    {


        private readonly IListService _service;

        private readonly ICurrentUserService _currentUser;

        public Api_PaginatedListDataController(IListService service, ICurrentUserService currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpPost("trips-paginated")]
        public async Task<CustomApiResponse> GetTripData_Paginated([FromBody] PaginationParameterDTO param)
        {
            var response = new CustomApiResponse();
            try
            {
                var data = await _service.Get_PaginatedTripList(
                    
                    listType: param.ListType  , pagesize: param.pagesize , pagenumber: param.pagenumber, filtertext: param.filtertext, CustomerId: param.CustomerId
                    ,Year:param.Year
                    );
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

    public class PaginationParameterDTO
    {
        public int Year { get; set; }
        public int CustomerId { get; set; } = 0;        

        public string ListType { get; set; }
        public string filtertext { get; set; } = "";
        public int pagesize { get; set; } = 25;
        public int pagenumber { get; set; } = 25;
    }


}
