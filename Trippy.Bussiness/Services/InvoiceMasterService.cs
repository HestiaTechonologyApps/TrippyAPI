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
    public class InvoiceMasterService : IInvoiceMasterService
    {
        private readonly IInvoiceMasterRepository _repo;
        private readonly IAuditRepository _auditRepository;
        private readonly ICurrentUserService _currentUser;
        public String AuditTableName { get; set; } = "INVOICEMASTER";
        public InvoiceMasterService(IInvoiceMasterRepository repo, IAuditRepository auditRepository, ICurrentUserService currentUserService)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
            _currentUser = currentUserService;
        }
        public async Task<invoiceMasterDTO> CreateAsync(InvoiceMaster invoiceMaster)
        {
            await _repo.AddAsync(invoiceMaster);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<InvoiceMaster>(
                tableName: AuditTableName,
                action: "create",
                recordId: invoiceMaster.InvoicemasterId,
                oldEntity: null,
                newEntity: invoiceMaster,
                changedBy: _currentUser.Email.ToString() // Replace with actual user info
            );
            return await ConvertInvoiceMasterToDTO(invoiceMaster);
        }

        private async Task<invoiceMasterDTO> ConvertInvoiceMasterToDTO(InvoiceMaster invoiceMaster)
        {
            invoiceMasterDTO invoicemasterDTO = new invoiceMasterDTO();
            invoicemasterDTO.InvoicemasterId = invoiceMaster.InvoicemasterId;
            invoicemasterDTO.InvoiceNum = invoiceMaster.InvoiceNum;
            invoicemasterDTO.FinancialYearId = invoiceMaster.FinancialYearId;
            invoicemasterDTO.CompanyId = invoiceMaster.CompanyId;
            invoicemasterDTO.TotalAmount = invoiceMaster.TotalAmount;
            invoicemasterDTO.CreatedOn = invoiceMaster.CreatedOn;
            invoicemasterDTO.CreatedOnString = invoiceMaster.CreatedOn.ToString("dd MMMM yyyy hh:mm tt");
            invoicemasterDTO.IsDeleted = invoiceMaster.IsDeleted;
            invoicemasterDTO.CreatedBy = "";

           // invoicemasterDTO.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync("InvoiceMaster", invoiceMaster.InvoicemasterId);
            return invoicemasterDTO;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var invoiceMaster = await _repo.GetByIdAsync(id);
            if (invoiceMaster == null) return false;
            _repo.Delete(invoiceMaster);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<InvoiceMaster>(
                tableName: AuditTableName,
                action: "Delete",
                recordId: invoiceMaster.InvoicemasterId,
                oldEntity: invoiceMaster,
                newEntity: invoiceMaster,
                changedBy: _currentUser.Email.ToString() // Replace with actual user info
            );
            return true;
        }
        public async Task<List<invoiceMasterDTO>> GetAllAsync()
        {


            List<invoiceMasterDTO> invoiceMasterdtos = new List<invoiceMasterDTO>();

            var invoiceMasters = await _repo.GetAllAsync();

            foreach (var invoiceMaster in invoiceMasters)
            {
                invoiceMasterDTO invoiceMasterDTO = await ConvertInvoiceMasterToDTO(invoiceMaster);
                invoiceMasterdtos.Add(invoiceMasterDTO);


            }

            return invoiceMasterdtos;
        }
        public async Task<invoiceMasterDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var invoiceMasterdto = await ConvertInvoiceMasterToDTO(q);
           // invoiceMasterdto.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync("InvoiceMasters", invoiceMasterdto.InvoicemasterId);

            return invoiceMasterdto;
        }
        public async Task<bool> UpdateAsync(InvoiceMaster invoiceMaster)
        {
            var oldentity = await _repo.GetByIdAsync(invoiceMaster.InvoicemasterId);
            _repo.Detach(oldentity);
            _repo.Update(invoiceMaster);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<InvoiceMaster>(
               tableName: AuditTableName,
               action: "update",
               recordId: invoiceMaster.InvoicemasterId,
               oldEntity: oldentity,
               newEntity: invoiceMaster,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;
        }
    }
}