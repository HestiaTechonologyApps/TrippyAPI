using Trippy.Domain.Entities;
using Trippy.Domain.DTO;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IDriverService
    {

        Task<List<DriverDTO>> GetAllAsync();
        Task<DriverDTO?> GetByIdAsync(int id);
        Task<DriverDTO> CreateAsync(Driver coupon);
        Task<bool> UpdateAsync(Driver coupon);
        Task<bool> DeleteAsync(int id);
        Task<CustomApiResponse> UpdateProfilePicAsync(int Driverid, string profileImagePath);

    }
}
