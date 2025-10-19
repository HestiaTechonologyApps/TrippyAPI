using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ICompanyBranchService
    {
       
            Task<List<CompanyBranch>> GetAllAsync();
            Task<CompanyBranch?> GetByIdAsync(int id);
            Task<CompanyBranch> CreateAsync(CompanyBranch coupon);
            Task<bool> UpdateAsync(CompanyBranch coupon);
            Task<bool> DeleteAsync(int id);
       
    }


}
