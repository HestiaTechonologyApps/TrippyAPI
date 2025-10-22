using Microsoft.AspNetCore.Mvc;
using Trippy.Domain.Entities;
using Trippy.Domain.DTO; // For CustomApiResponse
using System.Threading.Tasks;
using System.Collections.Generic;
using Trippy.Domain.Interfaces.IServices;

[ApiController]
[Route("api/[controller]/[action]")]
public class CompanyBranchController : ControllerBase
{
    private readonly ICompanyBranchService _service;
    public CompanyBranchController(ICompanyBranchService service)
    {
        _service = service;
    }
    /// <summary>
    /// get all company
    /// </summary>
    /// <returns></returns>
    [HttpGet("admin-getall-companybranch")]
    public async Task<CustomApiResponse> GetAll()
    {
        var response = new CustomApiResponse();
        try
        {
            var companybranchs = await _service.GetAllAsync();
            response.IsSucess = true;
            response.Value = companybranchs;
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
        var companybranch = await _service.GetByIdAsync(id);
        if (companybranch == null)
        {
            response.IsSucess = false;
            response.Error = "Not found";
            response.StatusCode = 404;
        }
        else
        {
            response.IsSucess = true;
            response.Value = companybranch;
            response.StatusCode = 200;
        }
        return response;
    }

    [HttpGet("admin-lookUp-companyBranch")]
    public async Task<CustomApiResponse> GetCompanyLookUp()
    {
        var response = new CustomApiResponse();
        try
        {
            var companys = await _service.GetAllAsync();

            var lookup = new List<LookUpDTO>();
            foreach (var company in companys)
            {
                lookup.Add(new LookUpDTO { Id = company.CompanyBranchId, Text = company.BranchName });
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


    [HttpPost]
    public async Task<CustomApiResponse> Create([FromBody] CompanyBranch companybranch)
    {
        var response = new CustomApiResponse();
        try
        {
            var created = await _service.CreateAsync(companybranch);
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
    public async Task<CustomApiResponse> Update(int id, [FromBody] CompanyBranch companybranch)
    {
        var response = new CustomApiResponse();
        if (id != companybranch.CompanyBranchId)
        {
            response.IsSucess = false;
            response.Error = "Id mismatch";
            response.StatusCode = 400;
            return response;
        }
        var updated = await _service.UpdateAsync(companybranch);
        if (!updated)
        {
            response.IsSucess = false;
            response.Error = "Not found";
            response.StatusCode = 404;
        }
        else
        {
            response.IsSucess = true;
            response.Value = companybranch;
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
