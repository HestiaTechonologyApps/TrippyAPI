using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trippy.Domain.DTO; // For CustomApiResponse
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IServices;
using TrippyAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuditController : Api_BaseController
{
    private readonly IAuditLogService _service;
    public AuditController(IAuditLogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<CustomApiResponse> GetAll()
    {
        var response = new CustomApiResponse();
        try
        {
            var categoryTaxs = await _service.GetAllLogsAsync();
            response.IsSucess = true;
            response.Value = categoryTaxs;
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
