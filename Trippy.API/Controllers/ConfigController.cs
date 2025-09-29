using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IServices; // Add this using directive

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfigurationServices _configurationServices;

    public ConfigController(IConfigurationServices configurationServices)
    {
        _configurationServices = configurationServices;
    }

    [HttpGet("languages")]
    public async Task<CustomApiResponse> GetLanguages()
    {
        var response = new CustomApiResponse();
        var languages = await _configurationServices.GetMasterLanguagesAsync();
        if (languages == null || languages.Count == 0)
        {
            response.IsSucess = false;
            response.Error = "languages.json not found or empty";
            response.StatusCode = 404;
            return response;
        }

        response.IsSucess = true;
        response.Value = languages;
        response.StatusCode = 200;
        return response;
    }

    [HttpGet("interests")]
    public async Task<CustomApiResponse> GetInterests()
    {
        var response = new CustomApiResponse();
        var interests = await _configurationServices.GetMasterInterestsAsync();
        if (interests == null || interests.Count == 0)
        {
            response.IsSucess = false;
            response.Error = "interests.json not found or empty";
            response.StatusCode = 404;
            return response;
        }

        response.IsSucess = true;
        response.Value = interests;
        response.StatusCode = 200;
        return response;
    }

    [HttpGet("gifts")]
    public async Task<CustomApiResponse> GetGifts()
    {
        var response = new CustomApiResponse();
        var gifts = await _configurationServices.GetMasterGiftsAsync();
        if (gifts == null || gifts.Count == 0)
        {
            response.IsSucess = false;
            response.Error = "interests.json not found or empty";
            response.StatusCode = 404;
            return response;
        }

        response.IsSucess = true;
        response.Value = gifts;
        response.StatusCode = 200;
        return response;
    }

}