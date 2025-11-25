using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class ExpenseMasterService : IExpenseMasterService
    {
        private readonly IExpenseMasterRepository _repo;
        private readonly IAuditRepository auditRepository;
        private readonly ICurrentUserService _currentUser;

        public String AuditTableName { get; set; } = "EXPENSEMASTER";
        public ExpenseMasterService(IExpenseMasterRepository repo, IAuditRepository auditRepository, ICurrentUserService currentUser)
        {
            _repo = repo;
            this.auditRepository = auditRepository;
            _currentUser = currentUser;
        }

        public async Task<List<ExpenseMarkDTO>> GetAllAsync()
        {
           // List<ExpenseMarkDTO> expenseMasterdtos = new List<ExpenseMarkDTO>();

            var expenseMasters =  _repo.GetAllExpenses();

            //foreach (var expenseMaster in expenseMasters)
            //{
            //    ExpenseMarkDTO expenseMasterDTO = await ConvertExpenseMasterToDTO(expenseMaster);
            //    expenseMasterdtos.Add(expenseMasterDTO);
            //}

            return expenseMasters;
        }

        public async Task<ExpenseMarkDTO?> GetByIdAsync(int id)
        {
            var q =  _repo.GetExpenseDetails(id);
            //if (q == null) return null;
            //var expenseTypeDTO = await ConvertExpenseMasterToDTO(q);
            return q;
        }

        public async Task<ExpenseMarkDTO> CreateAsync(ExpenseMaster expenseMaster)
        {
            if(expenseMaster.ExpenseVoucher == null || expenseMaster.ExpenseVoucher == "")
            {
                expenseMaster.ExpenseVoucher = await _repo.GenerateExpenseVoucherNumberAsync(expenseMaster);
            }
            expenseMaster.CompanyId = int.Parse(_currentUser.CompanyId);
            await _repo.AddAsync(expenseMaster);
            await _repo.SaveChangesAsync();
            await this.auditRepository.LogAuditAsync<ExpenseMaster>(
                tableName: AuditTableName,
                action: "create",
                recordId: expenseMaster.ExpenseMasterId,
                oldEntity: null,
                newEntity: expenseMaster,
                changedBy: _currentUser.Email.ToString() // Replace with actual user info
            );
            return await ConvertExpenseMasterToDTO(expenseMaster);
        }

        private async Task<ExpenseMarkDTO> ConvertExpenseMasterToDTO(ExpenseMaster expenseMaster)
        {
            ExpenseMarkDTO expenseMasterDTO = new ExpenseMarkDTO();
            expenseMasterDTO.ExpenseTypeId = expenseMaster.ExpenseTypeId;
            
            expenseMasterDTO.Remark = expenseMaster.Remark;
            expenseMasterDTO.ExpenseMasterId = expenseMaster.ExpenseMasterId;
            expenseMasterDTO.CreatedOn = expenseMaster.CreatedOn;
            expenseMasterDTO.CreatedOnString = expenseMaster.CreatedOn.ToString("yyyy-MM-dd");
            expenseMasterDTO.IsActive = expenseMaster.IsActive;
            expenseMasterDTO.Amount = expenseMaster.Amount;
            expenseMasterDTO.RelatedEntityId = expenseMaster.RelatedEntityId;
            expenseMaster.RelatedEntityType = expenseMaster.RelatedEntityType;
            expenseMasterDTO.PaymentMode = expenseMaster.PaymentMode;
            expenseMasterDTO.IsDeleted = expenseMaster.IsDeleted;
            expenseMasterDTO.CompanyId = expenseMaster.CompanyId;
            return expenseMasterDTO;
        }

        public async Task<bool> UpdateAsync(ExpenseMaster expenseMaster)
        {
            var oldentity = await _repo.GetByIdAsync(expenseMaster.ExpenseMasterId);
            _repo.Detach(oldentity);
            _repo.Update(expenseMaster);
            expenseMaster.CompanyId = int.Parse(_currentUser.CompanyId);
            await _repo.SaveChangesAsync();
            await auditRepository.LogAuditAsync<ExpenseMaster>(
               tableName: AuditTableName,
               action: "update",
               recordId: expenseMaster.ExpenseMasterId,
               oldEntity: oldentity,
               newEntity: expenseMaster,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var expenseMaster = await _repo.GetByIdAsync(id);
            if (expenseMaster == null) return false;
            _repo.Delete(expenseMaster);
            await _repo.SaveChangesAsync();
            await auditRepository.LogAuditAsync<ExpenseMaster>(
                tableName: AuditTableName,
                action: "Delete",
                recordId: expenseMaster.ExpenseTypeId,
                oldEntity: expenseMaster,
                newEntity: expenseMaster,
                changedBy: _currentUser.Email.ToString() // Replace with actual user info
            );
            return true;
        }

        public async Task<List<ExpenseMasterAuditDTO>> GetExpenseMasterForEntityAsync(string tableName, int recordId)
        {
            return await _repo.GetExpenseLogsForEntityAsync(tableName, recordId);
        }
    }
}
