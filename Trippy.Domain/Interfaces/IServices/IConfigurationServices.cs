using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IConfigurationServices
    {
        Task<List<LanguageDto>> GetMasterLanguagesAsync();
        Task<List<InterestDto>> GetMasterInterestsAsync();
        Task<List<GiftDto>> GetMasterGiftsAsync();
    }



    

}
