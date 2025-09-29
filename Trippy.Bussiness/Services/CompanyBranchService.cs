using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class CompanyBranchService : ICompanyBranchService
    {
        private readonly ICompanyBranchRepository _repo;
        public CompanyBranchService(ICompanyBranchRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CompanyBranch>> GetAllAsync() => (List<CompanyBranch>)await _repo.GetAllAsync();

        public async Task<CompanyBranch?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<CompanyBranch> CreateAsync(CompanyBranch companybranch)
        {
            await _repo.AddAsync(companybranch);
            await _repo.SaveChangesAsync();
            return companybranch;
        }

        public async Task<bool> UpdateAsync(CompanyBranch companybranch)
        {
            _repo.Update(companybranch);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var companybranch = await _repo.GetByIdAsync(id);
            if (companybranch == null) return false;
            _repo.Delete(companybranch);
            await _repo.SaveChangesAsync();
            return true;
        }
    }


}
