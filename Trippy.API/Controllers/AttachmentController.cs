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
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IWebHostEnvironment _env;
        public AttachmentController(IAttachmentService attachmentService, IWebHostEnvironment env)
        {
            _attachmentService = attachmentService;
            _env = env;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<CustomApiResponse>> Upload([FromForm] AttachmentUploadRequestDTO request)
        {
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", request.TableName,request.RecordId.ToString ());
            // var response = await _attachmentService.UploadFileAsync(file, uploadPath,tableName, recordId, description);

            var response = await _attachmentService.UploadFileAsync(
       request.File,
         uploadPath,
       request.TableName,
       request.RecordId,
       request.Description);
            return Ok(response);
        }

        // Get attachments by RecordID
        [HttpGet("{tableName}/{recordId}")]
        public async Task<CustomApiResponse> GetAttachments(string tableName, int recordId)
        {
            var response = await _attachmentService.GetAttachmentsAsync(tableName, recordId);
            return response;
        }

        // Soft delete attachment
        [HttpDelete("{attachmentId}")]
        public async Task<CustomApiResponse> Delete(int attachmentId, [FromQuery] string deletedBy)
        {
            var response = await _attachmentService.DeleteAttachmentAsync(attachmentId, deletedBy);
            return response;
            
        }

        [HttpGet("download/{attachmentId}")]
        public async Task<IActionResult> Download(int attachmentId)
        {
            var result = await _attachmentService.DownloadAttachmentAsync(attachmentId);

            if (result.ErrorMessage != null)
                return NotFound(ApiResponseFactory.Fail(result.ErrorMessage, System.Net.HttpStatusCode.NotFound));

            return File(result.FileStream!, result.ContentType!, result.FileName!);
        }
    }

    public class AttachmentUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public int RecordId { get; set; }

        public string Description { get; set; } = "";
    }
}
