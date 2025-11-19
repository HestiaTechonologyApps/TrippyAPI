using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trippy.Domain.DTO;

namespace TrippyAPI.Controllers
{
    public class Api_BaseController : ControllerBase
    {
       

        public String CurrentUserID = "0";
        public String EntityName = "";




        public Api_BaseController()
        {
        }

       

    }


 

}
