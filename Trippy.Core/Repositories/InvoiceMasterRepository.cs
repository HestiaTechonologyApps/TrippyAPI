using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class InvoiceMasterRepository : GenericRepository<InvoiceMaster>, IInvoiceMasterRepository
    {
        private readonly AppDbContext _context;
        public InvoiceMasterRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<InvoiceMasterDTO> QuerableInvoiceMasterListAsyc()
        {
            var query = from im in _context.InvoiceMasters.AsNoTracking()
                        join fy in _context.FinancialYears.AsNoTracking()
                        on im.FinancialYearId equals fy.FinancialYearId
                        join cust in _context.Customers.AsNoTracking()
                        on im.CustomerId equals cust.CustomerId
                        join cmp in _context.Companies.AsNoTracking() on im.CompanyId equals cmp.CompanyId
                        where im.IsDeleted == false
                        select new InvoiceMasterDTO
                        {
                            InvoicemasterId = im.InvoicemasterId,
                            InvoiceNum = im.InvoiceNum,
                            FinancialYearId = im.FinancialYearId,
                           IsCompleted = im.IsCompleted,
                            CompanyId = im.CompanyId,
                            CompanyName = cmp.ComapanyName,
                            TotalAmount = im.TotalAmount,
                            CreatedOn = im.CreatedOn,
                            IsDeleted = im.IsDeleted
                            
                        };
            return query.AsQueryable();
        }


    }

}
