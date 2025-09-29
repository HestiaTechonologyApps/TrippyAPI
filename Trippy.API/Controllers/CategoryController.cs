using Microsoft.AspNetCore.Mvc;
using Trippy.Domain.Entities;
using Trippy.Domain.DTO; // For CustomApiResponse
using System.Threading.Tasks;
using System.Collections.Generic;
using Trippy.Domain.Interfaces.IServices;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet("admin-getall-category")]
    public async Task<CustomApiResponse> GetAll()
    {
        var response = new CustomApiResponse();
        try
        {
            var categorys = await _service.GetAllCategoryDTOAsync();
            response.IsSucess = true;
            response.Value = categorys;
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


    [HttpGet("admin-lookUp-category")]
    public async Task<CustomApiResponse> GetCategoryLookUp()
    {
        var response = new CustomApiResponse();
        try
        {
            var categories = await _service.GetAllAsync();

            var lookup = new List<LookUpDTO>();
            foreach (var cat in categories)
            {
                lookup.Add(new LookUpDTO { Id = cat.CategoryId, Text = cat.CategoryName, Code=cat.CategoryCode  });
            }
            response.IsSucess = true;
            response.Value = lookup;
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
        var category = await _service.GetByIdAsync(id);
        if (category == null)
        {
            response.IsSucess = false;
            response.Error = "Not found";
            response.StatusCode = 404;
        }
        else
        {
            response.IsSucess = true;
            response.Value = category;
            response.StatusCode = 200;
        }
        return response;
    }

    [HttpPost]
    public async Task<CustomApiResponse> Create([FromBody] Category category)
    {
        var response = new CustomApiResponse();
        try
        {
            var created = await _service.CreateAsync(category);
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
    public async Task<CustomApiResponse> Update(int id, [FromBody] Category category)
    {
        var response = new CustomApiResponse();
        if (id != category.CategoryId)
        {
            response.IsSucess = false;
            response.Error = "Id mismatch";
            response.StatusCode = 400;
            return response;
        }
        var updated = await _service.UpdateAsync(category);
        if (!updated)
        {
            response.IsSucess = false;
            response.Error = "Not found";
            response.StatusCode = 404;
        }
        else
        {
            response.IsSucess = true;
            response.Value = category;
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
}
