using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<bool> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(int id);

        Task<CustomApiResponse> DeleteCommentAsync(int commentId, string deletedBy);

        Task<CustomApiResponse> GetCommentAsync(string tableName , int recordId);
        Task<CustomApiResponse> AddCommentAsync(
       string description,
       string tableName,
       int recordId,
       string createdBy,
       bool isInternal,
       
      
       int parentCommentId = 0
   );
    }
}
