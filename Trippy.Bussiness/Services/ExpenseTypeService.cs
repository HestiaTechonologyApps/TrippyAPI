using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Core.Repositories;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _repo;
        private readonly IAuditRepository auditRepository;

        public String AuditTableName { get; set; } = "EXPENSETYPE";
        public ExpenseTypeService(IExpenseTypeRepository repo, IAuditRepository auditRepository)
        {
            _repo = repo;
            this.auditRepository = auditRepository;
        }

        public async Task<ExpenseTypeDTO> CreateAsync(ExpenseType expenseType)
        {

            await _repo.AddAsync(expenseType);
            await _repo.SaveChangesAsync();
            await this.auditRepository.LogAuditAsync<ExpenseType>(
                tableName: AuditTableName,
                action: "create",
                recordId: expenseType.ExpenseTypeId,
                oldEntity: null,
                newEntity: expenseType,
                changedBy: "System" // Replace with actual user info
            );
            return await ConvertExpenseTypeToDTO(expenseType);
        }

        private async Task<ExpenseTypeDTO> ConvertExpenseTypeToDTO(ExpenseType expenseType)
        {
            ExpenseTypeDTO expenseTypeDTO = new ExpenseTypeDTO();
            expenseTypeDTO.ExpenseTypeId = expenseType.ExpenseTypeId;
            expenseTypeDTO.ExpenseTypeName = expenseType.ExpenseTypeName;
            expenseTypeDTO.Description = expenseType.Description;
            expenseTypeDTO.IsActive = expenseType.IsActive;
            expenseTypeDTO.IsDeleted = expenseType.IsDeleted;
            return expenseTypeDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var expenseType = await _repo.GetByIdAsync(id);
            if (expenseType == null) return false;
            _repo.Delete(expenseType);
            await _repo.SaveChangesAsync();
            await auditRepository.LogAuditAsync<ExpenseType>(
                tableName: AuditTableName,
                action: "Delete",
                recordId: expenseType.ExpenseTypeId,
                oldEntity: expenseType,
                newEntity: expenseType,
                changedBy: "System" // Replace with actual user info
            );
            return true;
        }

        public async Task<List<ExpenseTypeDTO>> GetAllAsync()
        {
            List<ExpenseTypeDTO> expenseTypedtos = new List<ExpenseTypeDTO>();

            var expenseTypes = await _repo.GetAllAsync();

            foreach (var expenseType in expenseTypes)
            {
                ExpenseTypeDTO expenseTypeDTO = await ConvertExpenseTypeToDTO(expenseType);
                expenseTypedtos.Add(expenseTypeDTO);  
            }

            return expenseTypedtos;

        }

        public async Task<ExpenseTypeDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var expenseTypeDTO = await ConvertExpenseTypeToDTO(q);
            return expenseTypeDTO;
        }

        public async Task<bool> UpdateAsync(ExpenseType expenseType)
        {
            var oldentity = await _repo.GetByIdAsync(expenseType.ExpenseTypeId);
            _repo.Detach(oldentity);
            _repo.Update(expenseType);
            await _repo.SaveChangesAsync();
            await auditRepository.LogAuditAsync<ExpenseType>(
               tableName: AuditTableName,
               action: "update",
               recordId: expenseType.ExpenseTypeId,
               oldEntity: oldentity,
               newEntity: expenseType,
               changedBy: "System" // Replace with actual user info
           );
            return true;
        }

        public async Task<List<MonthlyDashboardDTO>> GetMonthlyExpenseInvoiceAsync(int year)
        {
            return await _repo.GetMonthlyExpenseInvoiceAsync(year);
        }
    }
}
