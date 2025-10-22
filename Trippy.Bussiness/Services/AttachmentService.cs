using Microsoft.AspNetCore.Http;
using Trippy.Core.Helpers;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

public class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _repo;
    public AttachmentService(IAttachmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Attachment>> GetAllAsync() => (List<Attachment>)await _repo.GetAllAsync();



    public async Task<Attachment> CreateAsync(Attachment category)
    {
        await _repo.AddAsync(category);
        await _repo.SaveChangesAsync();
        return category;
    }

    public async Task<bool> UpdateAsync(Attachment category)
    {
        _repo.Update(category);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null) return false;
        _repo.Delete(category);
        await _repo.SaveChangesAsync();
        return true;
    }

     Task<Attachment?> IAttachmentService.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<(Stream? FileStream, string? FileName, string? ContentType, string? ErrorMessage)> DownloadAttachmentAsync(int attachmentId)
    {
        var attachment = await _repo.GetByIdAsync(attachmentId);
        if (attachment == null || attachment.IsDeleted)
            return (null, null, null, "Attachment not found");

        if (!File.Exists(attachment.FilePath))
            return (null, null, null, "File not found on server");

        var fileStream = new FileStream(attachment.FilePath, FileMode.Open, FileAccess.Read);
        var contentType = GetContentType(attachment.FileName);
        return (fileStream, attachment.FileName, contentType, null);
    }

    private string GetContentType(string fileName)
    {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
            contentType = "application/octet-stream";
        return contentType;
    }
    public async Task<CustomApiResponse> UploadFileAsync(IFormFile file, string uploadPath, string tableName, int recordId, string description)
    {
        if (file == null || file.Length == 0)
            return ApiResponseFactory.Fail("File is null or empty", System.Net.HttpStatusCode.BadRequest);
        // return new CustomApiResponse { Success = false, Message = "File is empty" };


        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var attachment = new Attachment
        {
            TableName = tableName,
            RecordID = recordId,
            Description = description,
            FileName = file.FileName,
            FileType = Path.GetExtension(file.FileName),
            AttachmentType= Path.GetExtension(file.FileName),
            FilePath = filePath,
            AttachmentPath=filePath ,
            UploaddedOn = DateTime.UtcNow,
            UploadedBy = "System" // replace with actual user
        };

        await _repo.AddAsync(attachment);

        await _repo.SaveChangesAsync();
        return ApiResponseFactory.Success(attachment, "File uploaded successfully", System.Net.HttpStatusCode.OK);
        // return new CustomApiResponse { Success = true, Message = "File uploaded successfully", Data = attachment };
    }

    public async Task<CustomApiResponse> GetAttachmentsAsync(string tableName, int recordId)
    {
        var attachments = await _repo.FindAsync (a => a.TableName == tableName && a.RecordID == recordId && !a.IsDeleted);
         return ApiResponseFactory.Success(attachments, string.Empty, System.Net.HttpStatusCode.OK);

        
    }

    public async Task<CustomApiResponse> DeleteAttachmentAsync(int attachmentId, string deletedBy)
    {
        var attachment = await _repo.GetByIdAsync(attachmentId);
        if (attachment == null)
            return ApiResponseFactory.Fail("Attachment not found", System.Net.HttpStatusCode.NotFound);
       

        attachment.IsDeleted = true;
        attachment.DeletedOn = DateTime.UtcNow;
        attachment.DeletedBy = deletedBy;

         _repo.Update(attachment);
        await _repo.SaveChangesAsync();
        return  ApiResponseFactory.Success(null, "Attachment deleted successfully", System.Net.HttpStatusCode.OK);
       
    }
}