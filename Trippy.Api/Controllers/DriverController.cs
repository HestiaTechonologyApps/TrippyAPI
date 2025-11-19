using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trippy.Domain.DTO; // For CustomApiResponse
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IServices;
using TrippyAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DriverController : Api_BaseController
{
    private readonly IDriverService _service;
    public DriverController(IDriverService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<CustomApiResponse> GetAll()
    {
        var response = new CustomApiResponse();
        try
        {
            var drivers = await _service.GetAllAsync();
            response.IsSucess = true;
            response.Value = drivers;
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
        var driver = await _service.GetByIdAsync(id);
        if (driver == null)
        {
            response.IsSucess = false;
            response.Error = "Not found";
            response.StatusCode = 404;
        }
        else
        {
            response.IsSucess = true;
            response.Value = driver;
            response.StatusCode = 200;
        }
        return response;
    }

    [HttpPost]
    public async Task<CustomApiResponse> Create([FromBody] Driver driver)
    {
        var response = new CustomApiResponse();
        try
        {
            var created = await _service.CreateAsync(driver);
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
    public async Task<CustomApiResponse> Update(int id, [FromBody] Driver driver)
    {
        var response = new CustomApiResponse();
        if (id != driver.DriverId)
        {
            response.IsSucess = false;
            response.Error = "Id mismatch";
            response.StatusCode = 400;
            return response;
        }
        var updated = await _service.UpdateAsync(driver);
        if (!updated)
        {
            response.IsSucess = false;
            response.Error = "Not found";
            response.StatusCode = 404;
        }
        else
        {
            response.IsSucess = true;
            response.Value = driver;
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


    /// <summary>
    /// Uploads the Profile Picture of the user
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("upload-profile-pic")]
    [Consumes("multipart/form-data")]
    public async Task<CustomApiResponse> UploadProfilePic([FromForm] ProfilePicUploadDto dto)
    {
        var appUserId = dto.AppUserId;
        var profilePic = dto.ProfilePic;

        if (profilePic == null || profilePic.Length == 0)
            return new CustomApiResponse { IsSucess = false, Error = "No file uploaded", StatusCode = 400 };

        // Check file size (max 2MB)
        const long maxFileSize = 2 * 1024 * 1024;
        if (profilePic.Length > maxFileSize)
            return new CustomApiResponse { IsSucess = false, Error = "File size exceeds 2MB", StatusCode = 400 };

        // Check file type (allow only images and gifs)
        var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
        if (!allowedContentTypes.Contains(profilePic.ContentType.ToLower()))
            return new CustomApiResponse { IsSucess = false, Error = "Only image files (jpg, png, gif, webp) are allowed", StatusCode = 400 };

        // Get user to check for old profile pic
        var user = await _service.GetByIdAsync(appUserId);
        if (user == null)
            return new CustomApiResponse { IsSucess = false, Error = "User not found", StatusCode = 404 };

        // Prepare file path
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilepics");
        Directory.CreateDirectory(uploadsFolder);

        var fileExtension = Path.GetExtension(profilePic.FileName);
        var fileName = $"{appUserId}_{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        // Delete old profile pic if exists and is not empty
        if (!string.IsNullOrEmpty(user.ProfileImagePath))
        {
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfileImagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (System.IO.File.Exists(oldFilePath))
            {
                try { System.IO.File.Delete(oldFilePath); } catch { /* ignore file delete errors */ }
            }
        }

        // Save new file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await profilePic.CopyToAsync(stream);
        }

        // Save relative path to DB
        var relativePath = $"/profilepics/{fileName}";
        var result = await _service.UpdateProfilePicAsync(appUserId, relativePath);

        return result;
    }


}
