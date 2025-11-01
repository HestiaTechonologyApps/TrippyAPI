﻿using System;
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

        public String AuditTableName { get; set; } = "EXPENSEMASTER";
        public ExpenseMasterService(IExpenseMasterRepository repo, IAuditRepository auditRepository)
        {
            _repo = repo;
            this.auditRepository = auditRepository;
        }

        public async Task<List<ExpenseMarkDTO>> GetAllAsync()
        {
            List<ExpenseMarkDTO> expenseMasterdtos = new List<ExpenseMarkDTO>();

            var expenseMasters = await _repo.GetAllAsync();

            foreach (var expenseMaster in expenseMasters)
            {
                ExpenseMarkDTO expenseMasterDTO = await ConvertExpenseMasterToDTO(expenseMaster);
                expenseMasterdtos.Add(expenseMasterDTO);
            }

            return expenseMasterdtos;
        }

        public async Task<ExpenseMarkDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var expenseTypeDTO = await ConvertExpenseMasterToDTO(q);
            return expenseTypeDTO;
        }

        public async Task<ExpenseMarkDTO> CreateAsync(ExpenseMaster expenseMaster)
        {
            await _repo.AddAsync(expenseMaster);
            await _repo.SaveChangesAsync();
            await this.auditRepository.LogAuditAsync<ExpenseMaster>(
                tableName: AuditTableName,
                action: "create",
                recordId: expenseMaster.ExpenseTypeId,
                oldEntity: null,
                newEntity: expenseMaster,
                changedBy: "System" // Replace with actual user info
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
            expenseMasterDTO.IsActive = expenseMaster.IsActive;
            expenseMasterDTO.IsDeleted = expenseMaster.IsDeleted;
            return expenseMasterDTO;
        }

        public async Task<bool> UpdateAsync(ExpenseMaster expenseMaster)
        {
            var oldentity = await _repo.GetByIdAsync(expenseMaster.ExpenseMasterId);
            _repo.Detach(oldentity);
            _repo.Update(expenseMaster);
            await _repo.SaveChangesAsync();
            await auditRepository.LogAuditAsync<ExpenseMaster>(
               tableName: AuditTableName,
               action: "update",
               recordId: expenseMaster.ExpenseMasterId,
               oldEntity: oldentity,
               newEntity: expenseMaster,
               changedBy: "System" // Replace with actual user info
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
                changedBy: "System" // Replace with actual user info
            );
            return true;
        }
    }
}
