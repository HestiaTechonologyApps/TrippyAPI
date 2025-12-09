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
                where im.IsDeleted == false
                select new
                {
                    im.InvoiceMasterId,
                    im.InvoiceNum,
                    im.FinancialYearId,
                    im.IsCompleted,
                    im.CompanyId,
                    ComapanyName = cmp.ComapanyName,
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
                    InvoiceMasterId = x.InvoiceMasterId,
                    InvoiceNum = x.InvoiceNum,
                    FinancialYearId = x.FinancialYearId,
                    IsCompleted = x.IsCompleted,
                    CompanyId = x.CompanyId,
                    CustomerId = x.CustomerId,
                    ComapanyName = x.ComapanyName,
                    CustomerName = x.CustomerName,
                    TotalAmount = x.TotalAmount,
                    CreatedOn = x.CreatedOn,
                    InvoiceDate = x.InvoiceDate,
                    IsDeleted = x.IsDeleted
                });
        }
        public async Task<List<InvoiceMaster>> GetAllAsync()
        {
            return await _context.InvoiceMasters
                .Include(x => x.InvoiceDetails)   
                .ToListAsync();
        }
        public async Task<InvoiceMaster?> GetByIdAsync(int id)
        {
            return await _context.InvoiceMasters
                .Include(x => x.InvoiceDetails)   
                .FirstOrDefaultAsync(x => x.InvoiceMasterId == id);
        }

        public async Task<InvoiceMasterDTO?> GetByIdWithDetailsAsync(int id)
        {
            InvoiceMasterDTO invoiceMasterDTO1 = null;

            var Invmaster = from x in _context.InvoiceMasters
                            join com in _context.Companies on x.CompanyId equals com.CompanyId
                            join cust in _context.Customers on x.CustomerId equals cust.CustomerId
                            select new InvoiceMasterDTO
                            {
                                InvoiceMasterId = x.InvoiceMasterId,
                                InvoiceNum = x.InvoiceNum,
                                FinancialYearId = x.FinancialYearId,
                                IsCompleted = x.IsCompleted,
                                CompanyId = x.CompanyId,
                                CompanyName = com.ComapanyName,
                                CustomerName = cust.CustomerName,
                                TotalAmount = x.TotalAmount,
                                CreatedOn = x.CreatedOn,
                                InvoiceDate = x.InvoiceDate,
                                IsDeleted = x.IsDeleted

                            };
            invoiceMasterDTO1 = await Invmaster.FirstOrDefaultAsync();
            if (invoiceMasterDTO1 != null)
            {



                var invoicedetails = from x in _context.InvoiceDetails
                                     join trp in _context.TripOrders on x.TripOrderId equals trp.TripOrderId
                                     where x.InvoiceMasterId == invoiceMasterDTO1.InvoiceMasterId

                                     select new InvoiceDetailDTO
                                     {
                                         InvoiceDetailId = x.InvoiceDetailId,
                                         InvoiceMasterId = x.InvoiceMasterId,
                                         TripOrderId = x.TripOrderId,
                                         TripCode = trp.TripCode,
                                         CategoryId = x.CategoryId,
                                         Ammount = x.Ammount,
                                         TotalTax = x.TotalTax,
                                         Discount = x.Discount
                                     };


                invoiceMasterDTO1.InvoiceDetailDtos = await invoicedetails.ToListAsync();
            }



            return invoiceMasterDTO1;




        }

    }
}
