using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        // Get attachments by RecordID
        [HttpGet("{tableName}/{recordId}")]
        public async Task<CustomApiResponse> GetAttachments(string tableName, int recordId)
        {
            var response = await _commentService.GetCommentAsync(tableName, recordId);
            return response;
        }

        // Soft delete attachment
        [HttpDelete("{attachmentId}")]
        public async Task<CustomApiResponse> Delete(int commentId, [FromQuery] string deletedBy)
        {
            var response = await _commentService.DeleteCommentAsync(commentId, deletedBy);
            return response;

        }
        [HttpPost("AddComment")]
        public async Task<CustomApiResponse> AddComment([FromBody] Comment comment)
        {
            var response = new CustomApiResponse();
            try
            {
                var created = await _commentService.CreateAsync(comment);
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
