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

        public List<ExpenseMarkDTO> GetAllExpenses()
        {
            return (from exp in _context.ExpenseMasters
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
                        CreatedOnString = exp.CreatedOn.ToString("dd MMMM yyyy hh:mm tt"),
                        Remark = exp.Remark,
                        IsActive = exp.IsActive
                    }).ToList();
        }

        public ExpenseMarkDTO GetExpenseDetails(int expenseId)
        {
            var det= (from exp in _context.ExpenseMasters
                    join expType in _context.ExpenseTypes
                        on exp.ExpenseTypeId equals expType.ExpenseTypeId
                    where exp.ExpenseTypeId == expenseId
                    select new ExpenseMarkDTO
                    {
                        ExpenseMasterId = exp.ExpenseMasterId,
                        ExpenseTypeId = exp.ExpenseTypeId,
                        ExpenseTypeName = expType.ExpenseTypeName,
                        Amount = exp.Amount,
                        CreatedOnString = exp.CreatedOn.ToString("dd MMMM yyyy hh:mm tt"),
                        CreatedBy = exp.CreatedBy,
                        Remark = exp.Remark,
                        IsActive = exp.IsActive
                    }).FirstOrDefault();

            return det;
        }
    }
}
