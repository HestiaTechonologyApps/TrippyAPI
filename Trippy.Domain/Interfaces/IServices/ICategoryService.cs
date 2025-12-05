using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ICategoryService
    {
       
            Task<List<Category>> GetAllAsync();
            Task<List<CategoryDTO>> GetAllCategoryDTOAsync();
            Task<CategoryDTO?> GetByIdAsync(int id);
            Task<Category> CreateAsync(Category coupon);
            Task<bool> UpdateAsync(Category coupon);
            Task<bool> DeleteAsync(int id);
       
    }
}
