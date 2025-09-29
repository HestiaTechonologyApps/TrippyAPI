using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IRepositories
{
    public interface IFinancialYearRepository : IGenericRepository<FinancialYear>
    {



    }
    public interface ICompanyRepository : IGenericRepository<Company>
    {
    }




    public interface ICompanyBranchRepository : IGenericRepository<CompanyBranch>
    {
    }
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<CategoryDTO> GetCategoryDTO(int CategoryId);
        Task<List<CategoryDTO>> GetAllCategoryDTOs();
    }
    public interface ICategoryTaxRepository : IGenericRepository<CategoryTax>
    {
    }

    public interface IAppNotificationRepository : IGenericRepository<AppNotification>
    {
    }
}
