using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ICategoryTaxService
    {

        Task<List<CategoryTax>> GetAllAsync();
        Task<CategoryTax?> GetByIdAsync(int id);
        Task<CategoryTax> CreateAsync(CategoryTax coupon);
        Task<bool> UpdateAsync(CategoryTax coupon);
        Task<bool> DeleteAsync(int id);

    }


}
