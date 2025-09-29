using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.InfraCore.Repositories
{
    public class FinancialYearRepository : GenericRepository<FinancialYear>, IFinancialYearRepository
    {
        private readonly AppDbContext _context;
        public FinancialYearRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
