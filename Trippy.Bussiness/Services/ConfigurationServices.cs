using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Hosting; // Add this using directive


namespace Trippy.Bussiness.Services
{
    public class ConfigurationServices : IConfigurationServices
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMemoryCache _cache;
        public ConfigurationServices(IWebHostEnvironment env, IMemoryCache cache)
        {

            _env = env;
            _cache = cache;
        }


        public async Task<List<LanguageDto>> GetMasterLanguagesAsync()
        {
            return await _cache.GetOrCreateAsync("master_languages", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1); // Cache for 1 hour
                var filePath = Path.Combine(_env.WebRootPath, "data", "languages.json");
                if (!File.Exists(filePath))
                    return new List<LanguageDto>();
                var json = await File.ReadAllTextAsync(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<LanguageDto>>(json, options) ?? new List<LanguageDto>();
                
            });
        }

        public async Task<List<InterestDto>> GetMasterInterestsAsync()
        {
            return await _cache.GetOrCreateAsync("master_interests", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1); // Cache for 1 hour
                var filePath = Path.Combine(_env.WebRootPath, "data", "interests.json");
                if (!File.Exists(filePath))
                    return new List<InterestDto>();
                var json = await File.ReadAllTextAsync(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<InterestDto>>(json, options) ?? new List<InterestDto>();

               
            });
        }

        public async Task<List<GiftDto>> GetMasterGiftsAsync()
        {
            return await _cache.GetOrCreateAsync("master_gift", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1); // Cache for 1 hour
                var filePath = Path.Combine(_env.WebRootPath, "data", "gift.json");
                if (!File.Exists(filePath))
                    return new List<GiftDto>();
                var json = await File.ReadAllTextAsync(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<GiftDto>>(json, options) ?? new List<GiftDto>();


            });
        }




    }
}