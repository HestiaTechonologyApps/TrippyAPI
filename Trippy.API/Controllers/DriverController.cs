using Microsoft.AspNetCore.Mvc;
using Trippy.Domain.Entities;
using Trippy.Domain.DTO; // For CustomApiResponse
using System.Threading.Tasks;
using System.Collections.Generic;
using Trippy.Domain.Interfaces.IServices;

[ApiController]
[Route("api/[controller]/[action]")]
public class DriverController : ControllerBase
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
}
