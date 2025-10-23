using Microsoft.AspNetCore.Mvc;
using Trippy.Domain.Entities;
using Trippy.Domain.DTO; // For CustomApiResponse
using System.Threading.Tasks;
using System.Collections.Generic;
using Trippy.Domain.Interfaces.IServices;
using Trippy.Core.Helpers;
using System;

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
        try
        {
            var categorys = await _service.GetAllCategoryDTOAsync();
            return ApiResponseFactory.Success(categorys, string.Empty, System.Net.HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ApiResponseFactory.Exception(ex);
        }
    }

    //[HttpGet("admin-getall-categorydetails")]
    //public async Task<CustomApiResponse> categorydetails()
    //{
    //    try
    //    {
    //        var categorys = await _service.GetAllCategoryDTOAsync();
    //        return ApiResponseFactory.Success(categorys, string.Empty, System.Net.HttpStatusCode.OK);
    //    }
    //    catch (Exception ex)
    //    {
    //        return ApiResponseFactory.Exception(ex);
    //    }
    //}


    [HttpGet("admin-lookUp-category")]
    public async Task<CustomApiResponse> GetCategoryLookUp()
    {
        try
        {
            var categories = await _service.GetAllAsync();

            var lookup = new List<LookUpDTO>();
            foreach (var cat in categories)
            {
                lookup.Add(new LookUpDTO { Id = cat.CategoryId, Text = cat.CategoryName, Code = cat.CategoryCode });
            }

            return ApiResponseFactory.Success(lookup, string.Empty, System.Net.HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ApiResponseFactory.Exception(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<CustomApiResponse> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);
        if (category == null)
        {
            return ApiResponseFactory.Fail("Not found", System.Net.HttpStatusCode.NotFound);
        }

        return ApiResponseFactory.Success(category, string.Empty, System.Net.HttpStatusCode.OK);
    }

    [HttpPost]
    public async Task<CustomApiResponse> Create([FromBody] Category category)
    {
        try
        {
            var created = await _service.CreateAsync(category);
            return ApiResponseFactory.Success(created, "Created successfully", System.Net.HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ApiResponseFactory.Exception(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<CustomApiResponse> Update(int id, [FromBody] Category category)
    {
        if (id != category.CategoryId)
        {
            return ApiResponseFactory.Fail("Id mismatch", System.Net.HttpStatusCode.BadRequest);
        }

        var updated = await _service.UpdateAsync(category);
        if (!updated)
        {
            return ApiResponseFactory.Fail("Not found", System.Net.HttpStatusCode.NotFound);
        }

        return ApiResponseFactory.Success(category, "Updated successfully", System.Net.HttpStatusCode.OK);
    }

    [HttpDelete("{id}")]
    public async Task<CustomApiResponse> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
        {
            return ApiResponseFactory.Fail("Not found", System.Net.HttpStatusCode.NotFound);
        }

        return ApiResponseFactory.Success(null, "Deleted successfully", System.Net.HttpStatusCode.NoContent);
    }
}
