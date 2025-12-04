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
            var query =
                from im in _context.InvoiceMasters.AsNoTracking()
                join fy in _context.FinancialYears.AsNoTracking()
                    on im.FinancialYearId equals fy.FinancialYearId
                join cust in _context.Customers.AsNoTracking()
                    on im.CustomerId equals cust.CustomerId
                join cmp in _context.Companies.AsNoTracking()
                    on im.CompanyId equals cmp.CompanyId
                
                select new
                {
                    im.InvoicemasterId,
                    im.InvoiceNum,
                    im.FinancialYearId,
                    im.IsCompleted,
                    im.CompanyId,
                    CompanyName = cmp.ComapanyName,
                   im.CustomerId,
                   CustomerName =cust.CustomerName,
                    im.TotalAmount,
                    im.CreatedOn,
                    im.InvoiceDate,
                    im.IsDeleted
                };

            return query
                .Select(x => new InvoiceMasterDTO
                {
                    InvoicemasterId = x.InvoicemasterId,
                    InvoiceNum = x.InvoiceNum,
                    FinancialYearId = x.FinancialYearId,
                    IsCompleted = x.IsCompleted,
                    CompanyId = x.CompanyId,
                    CompanyName = x.CompanyName,
                    CustomerName = x.CustomerName,
                    TotalAmount = x.TotalAmount,
                    CreatedOn = x.CreatedOn,
                    InvoiceDate = x.InvoiceDate,
                    IsDeleted = x.IsDeleted
                });
        }



    }

}
