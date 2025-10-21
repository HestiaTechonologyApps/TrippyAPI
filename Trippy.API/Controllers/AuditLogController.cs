using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Trippy.Business.Services;
using Trippy.Core.Helpers;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditlogService;
        private readonly IWebHostEnvironment _env;
        public AuditLogController(IAuditLogService attachmentService, IWebHostEnvironment env)
        {
            _auditlogService = attachmentService;
            _env = env;
        }
        [HttpGet("{tableName}/{recordId}")]
        public async Task<CustomApiResponse> GetAuditLog(string tableName, int recordId)
        {
            var response = await _auditlogService.GetAuditLogsForEntityAsync(tableName, recordId);
            return ApiResponseFactory.Success(response);
        }
    }
}