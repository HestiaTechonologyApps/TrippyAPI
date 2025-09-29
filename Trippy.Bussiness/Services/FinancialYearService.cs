using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class FinancialYearService : IFinancialYearService
    {
        private readonly IFinancialYearRepository _repo;
        public FinancialYearService(IFinancialYearRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<FinancialYear>> GetAllAsync() => (List<FinancialYear>)await _repo.GetAllAsync();

        public async Task<FinancialYear?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<FinancialYear> CreateAsync(FinancialYear financialyear)
        {
            await _repo.AddAsync(financialyear);
            await _repo.SaveChangesAsync();
            return financialyear;
        }

        public async Task<bool> UpdateAsync(FinancialYear financialyear)
        {
            _repo.Update(financialyear);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var financialyear = await _repo.GetByIdAsync(id);
            if (financialyear == null) return false;
            _repo.Delete(financialyear);
            await _repo.SaveChangesAsync();
            return true;
        }
    }

    public class CategoryTaxService : ICategoryTaxService
    {
        private readonly ICategoryTaxRepository _repo;
        public CategoryTaxService(ICategoryTaxRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CategoryTax>> GetAllAsync() => (List<CategoryTax>)await _repo.GetAllAsync();

        public async Task<CategoryTax?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<CategoryTax> CreateAsync(CategoryTax categorytax)
        {
            await _repo.AddAsync(categorytax);
            await _repo.SaveChangesAsync();
            return categorytax;
        }

        public async Task<bool> UpdateAsync(CategoryTax categorytax)
        {
            _repo.Update(categorytax);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categorytax = await _repo.GetByIdAsync(id);
            if (categorytax == null) return false;
            _repo.Delete(categorytax);
            await _repo.SaveChangesAsync();
            return true;
        }
    }


}
