using Microsoft.EntityFrameworkCore;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.InfraCore.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
    public class CompanyBranchRepository : GenericRepository<CompanyBranch>, ICompanyBranchRepository
    {
        private readonly AppDbContext _context;
        public CompanyBranchRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        

        public Task<List<CategoryDTO>> GetAllCategoryDTOs()
        {
            return  (from c in _context.Categorys
                   join ct in _context.Companies on c.CompanyId equals ct.CompanyId 
                   select new CategoryDTO
                   {
                       CategoryId = c.CategoryId,
                       CompanyId=c.CompanyId,
                       CategoryName = c.CategoryName,
                       CategoryDescription = c.CategoryDescription,
                       CategoryTitle = c.CategoryTitle,
                       CategoryCode = c.CategoryCode,
                       CompanyName = ct.ComapanyName,
                       IsActive = ct.IsActive
                   }).ToListAsync();
        }

        public Task<CategoryDTO> GetCategoryDTO(int CategoryId)
        {
            return (from c in _context.Categorys
                    join ct in _context.Companies on c.CompanyId equals ct.CompanyId
                    where c.CategoryId == CategoryId
                    select new CategoryDTO
                    {
                        CategoryId = c.CategoryId,
                        CompanyId = c.CompanyId,
                        CategoryName = c.CategoryName,
                        CategoryDescription = c.CategoryDescription,
                        CategoryTitle = c.CategoryTitle,
                        CategoryCode = c.CategoryCode,
                        CompanyName = ct.ComapanyName,
                        IsActive = ct.IsActive
                    }).FirstOrDefaultAsync();
        }
    }
    public class CategoryTaxRepository : GenericRepository<CategoryTax>, ICategoryTaxRepository
    {
        private readonly AppDbContext _context;
        public CategoryTaxRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
    public class AppNotificationRepository : GenericRepository<AppNotification>, IAppNotificationRepository
    {
        private readonly AppDbContext _context;
        public AppNotificationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
    
}
