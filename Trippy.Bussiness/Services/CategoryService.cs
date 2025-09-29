using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Category>> GetAllAsync() => (List<Category>)await _repo.GetAllAsync();

        public async Task<List<CategoryDTO>> GetAllCategoryDTOAsync()
        {
            return (List<CategoryDTO>)await _repo.GetAllCategoryDTOs();
        }
        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category= await _repo.GetCategoryDTO(id);


            return category;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _repo.Update(category);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return false;
            _repo.Delete(category);
            await _repo.SaveChangesAsync();
            return true;
        }
    }





}
