using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IFinancialYearService
    {
        Task<List<FinancialYear>> GetAllAsync();
        Task<FinancialYear?> GetByIdAsync(int id);
        Task<FinancialYear> CreateAsync(FinancialYear coupon);
        Task<bool> UpdateAsync(FinancialYear coupon);
        Task<bool> DeleteAsync(int id);
    }
    public interface ICompanyService
    {
      
            Task<List<Company>> GetAllAsync();
            Task<Company?> GetByIdAsync(int id);
            Task<Company> CreateAsync(Company coupon);
            Task<bool> UpdateAsync(Company coupon);
            Task<bool> DeleteAsync(int id);
        
    }
    public interface ICompanyBranchService
    {
       
            Task<List<CompanyBranch>> GetAllAsync();
            Task<CompanyBranch?> GetByIdAsync(int id);
            Task<CompanyBranch> CreateAsync(CompanyBranch coupon);
            Task<bool> UpdateAsync(CompanyBranch coupon);
            Task<bool> DeleteAsync(int id);
       
    }
    public interface ICategoryService
    {
       
            Task<List<Category>> GetAllAsync();
            Task<List<CategoryDTO>> GetAllCategoryDTOAsync();
            Task<CategoryDTO?> GetByIdAsync(int id);
            Task<Category> CreateAsync(Category coupon);
            Task<bool> UpdateAsync(Category coupon);
            Task<bool> DeleteAsync(int id);
       
    }
    public interface ICategoryTaxService
    {
      
            Task<List<CategoryTax>> GetAllAsync();
            Task<CategoryTax?> GetByIdAsync(int id);
            Task<CategoryTax> CreateAsync(CategoryTax coupon);
            Task<bool> UpdateAsync(CategoryTax coupon);
            Task<bool> DeleteAsync(int id);
        
    }

    public interface IAppNotificationService
    {

        Task<List<AppNotification>> GetAllAsync();
        Task<AppNotification?> GetByIdAsync(int id);
        Task<AppNotification> CreateAsync(AppNotification coupon);
        Task<bool> UpdateAsync(AppNotification coupon);
        Task<bool> DeleteAsync(int id);

    }
}
