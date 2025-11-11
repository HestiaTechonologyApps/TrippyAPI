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
    public class ExpenseMasterRepository : GenericRepository<ExpenseMaster>, IExpenseMasterRepository
    {
        private readonly AppDbContext _context;
            public ExpenseMasterRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<string?> GenerateExpenseVoucherNumberAsync(ExpenseMaster expenseMaster)
        {

            var exptype = _context.ExpenseTypes.Where(e => e.ExpenseTypeId == expenseMaster.ExpenseTypeId).FirstOrDefault();
            if (exptype != null)
            {
                string prefix = exptype.ExpenseTypeCode; // Assuming ExpenseType has a property ExpenseTypeCode
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                // Get the last voucher number for the current month and year
                var lastVoucher = _context.ExpenseMasters
                    .Where(e => e.ExpenseVoucher.StartsWith($"{prefix}-{year}{month:00}"))
                    .OrderByDescending(e => e.ExpenseVoucher)
                    .FirstOrDefault();
                int nextNumber = 1;
                if (lastVoucher != null)
                {
                    // Extract the numeric part and increment
                    var parts = lastVoucher.ExpenseVoucher.Split('-');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
                string newVoucherNumber = $"{prefix}-{year}{month:00}-{nextNumber:0000}";
                return Task.FromResult<string?>(newVoucherNumber);
            }
            else
            {
                return Task.FromResult<string?>("");
            }
        }

        public List<ExpenseMarkDTO> GetAllExpenses()
        {
            var q= (from exp in _context.ExpenseMasters
                    join expType in _context.ExpenseTypes
                        on exp.ExpenseTypeId equals expType.ExpenseTypeId
                    select new ExpenseMarkDTO
                    {
                        ExpenseMasterId = exp.ExpenseMasterId,
                        ExpenseTypeId = exp.ExpenseTypeId,
                        ExpenseTypeName = expType.ExpenseTypeName,
                        Amount = exp.Amount,
                        CreatedOn = exp.CreatedOn,
                        IsDeleted = exp.IsDeleted,
                        RelatedEntityId= exp.RelatedEntityId,
                        RelatedTo = "",
                        RelatedEntityType = exp.RelatedEntityType,
                        ExpenseVoucher= exp.ExpenseVoucher,
                        PaymentMode= exp.PaymentMode,
                        CreatedOnString = exp.CreatedOn.ToString("dd MMMM yyyy hh:mm tt"),
                        Remark = exp.Remark,
                        IsActive = exp.IsActive
                    }).ToList();

            foreach (var expense in q)
            {
                if (expense.RelatedEntityType != null)
                {
                    if(expense.RelatedEntityType.ToLower() == "TripOrders".ToLower())
                    {
                        var trp= _context.TripOrders.Find(expense.RelatedEntityId);
                        if(trp != null)
                        {
                            expense.RelatedTo = trp.TripCode;
                        }   
                        

                    }
                    else if (expense.RelatedEntityType.ToLower() == "Driver".ToLower())
                    {
                        var trp = _context.Drivers.Find(expense.RelatedEntityId);
                        if (trp != null)
                        {
                            expense.RelatedTo = trp.DriverName;
                        }


                    }
                    else if (expense.RelatedEntityType.ToLower() == "vehicle".ToLower())
                    {
                        var trp = _context.Vehicles.Find(expense.RelatedEntityId);
                        if (trp != null)
                        {
                            expense.RelatedTo = trp.RegistrationNumber;
                        }


                    }

                }
            }

            return q;
        }

        public async Task<List<ExpenseMasterAuditDTO>> GetExpenseLogsForEntityAsync(string tableName, int recordId)
        { 
           var q= from exp in _context.ExpenseMasters join  expty in _context.ExpenseTypes
                  on exp.ExpenseTypeId equals expty.ExpenseTypeId
                  where exp.RelatedEntityType == tableName && exp.RelatedEntityId == recordId
                   orderby exp.CreatedOn descending
                   select new ExpenseMasterAuditDTO
                   {
                          ExpenseMasterId = exp.ExpenseMasterId,  
                            ExpenseTypeName = expty.ExpenseTypeName ,
                            ExpenseVoucher = exp.ExpenseVoucher,
                            Amount = exp.Amount,
                            CreatedBy = exp.CreatedBy,

                       CreatedOn = exp.CreatedOn.ToString("dd MMMM yyyy hh:mm tt")
                   };
            return  await q.ToListAsync ();
        }




        //public List<ExpenseMarkDTO> GetAllExpenses()
        //{
        //    var result = (
        //           from exp in _context.ExpenseMasters
        //           join expType in _context.ExpenseTypes
        //            on exp.ExpenseTypeId equals expType.ExpenseTypeId
        //            join trip in _context.TripOrders
        //                   on exp.RelatedEntityId equals trip.TripOrderId into tripGroup
        //                     from trip in tripGroup.DefaultIfEmpty()
        //              join mode in _context.TripBookingModes
        //                   on trip.TripBookingModeId equals mode.TripBookingModeId into modeGroup
        //              from mode in modeGroup.DefaultIfEmpty()
        //                 select new ExpenseMarkDTO
        //                 {
        //                     ExpenseMasterId = exp.ExpenseMasterId,
        //                     ExpenseTypeId = exp.ExpenseTypeId,
        //                      ExpenseTypeName = expType.ExpenseTypeName,
        //                        Amount = exp.Amount,
        //                         CreatedOn = exp.CreatedOn,
        //                     IsDeleted = exp.IsDeleted,
        //                    CreatedOnString = exp.CreatedOn.ToString("dd MMMM yyyy hh:mm tt"),
        //                    Remark = exp.Remark,
        //                     IsActive = exp.IsActive,
        //                      RelatedEntityId = exp.RelatedEntityId,
        //                     TripOrderId = trip != null ? trip.TripOrderId : 0,
        //                      TripBookingModeName = mode != null ? mode.TripBookingModeName : "",
        //                         TripAmount = trip != null ? trip.TripAmount : 0
        //                 }
        //              ).ToList();
        //    return result;
        //}

        public ExpenseMarkDTO GetExpenseDetails(int expenseId)
        {
            var det = (from exp in _context.ExpenseMasters
                       join expType in _context.ExpenseTypes
                           on exp.ExpenseTypeId equals expType.ExpenseTypeId
                       where exp.ExpenseTypeId == expenseId
                       select new ExpenseMarkDTO
                       {
                           ExpenseMasterId = exp.ExpenseMasterId,
                           ExpenseTypeId = exp.ExpenseTypeId,
                           ExpenseTypeName = expType.ExpenseTypeName,
                           Amount = exp.Amount,
                           CreatedOn = exp.CreatedOn,
                           IsDeleted = exp.IsDeleted,
                           RelatedEntityId = exp.RelatedEntityId,
                           RelatedTo = "",
                           RelatedEntityType = exp.RelatedEntityType,
                           ExpenseVoucher = exp.ExpenseVoucher,
                           PaymentMode = exp.PaymentMode,
                           CreatedOnString = exp.CreatedOn.ToString("dd MMMM yyyy hh:mm tt"),
                           Remark = exp.Remark,
                           IsActive = exp.IsActive
                       }).FirstOrDefault();
            return det;
        }
        
     
    }
}
