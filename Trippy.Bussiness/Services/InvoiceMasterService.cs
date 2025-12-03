using Microsoft.EntityFrameworkCore;
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
        private readonly ITripOrderRepository _tripOrderRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly ICurrentUserService _currentUser;
        public String AuditTableName { get; set; } = "INVOICEMASTER";
        public InvoiceMasterService(IInvoiceMasterRepository repo, IAuditRepository auditRepository, ICurrentUserService currentUserService, ITripOrderRepository tripOrderRepository)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
            _currentUser = currentUserService;
            _tripOrderRepository = tripOrderRepository;
        }
        public async Task<InvoiceMasterDTO> CreateAsync(InvoiceMasterDTO invoiceMasterDTO)
        {
            InvoiceMaster invoiceMaster = new InvoiceMaster();
            invoiceMaster.InvoiceNum = invoiceMasterDTO.InvoiceNum;
            invoiceMaster.FinancialYearId = invoiceMasterDTO.FinancialYearId;
            invoiceMaster.CompanyId = invoiceMasterDTO.CompanyId;
            invoiceMaster.TotalAmount = invoiceMasterDTO.TotalAmount;
            invoiceMaster.CreatedOn = DateTime.UtcNow;
            invoiceMaster.IsDeleted = false;


            foreach (var detailDto in invoiceMasterDTO.InvoiceDetailDtos)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.TripOrderId = detailDto.TripOrderId;
                invoiceDetail.CategoryId = detailDto.CategoryId;
                invoiceDetail.Ammount = detailDto.Ammount;
                invoiceDetail.TotalTax = detailDto.TotalTax;
                invoiceDetail.Discount = detailDto.Discount;
                invoiceMaster.InvoiceDetails.Add(invoiceDetail);

                // Here you can also handle InvoiceDetailTaxDTO if needed


            }






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

        private async Task<InvoiceMasterDTO> ConvertInvoiceMasterToDTO(InvoiceMaster invoiceMaster)
        {
            InvoiceMasterDTO invoicemasterDTO = new InvoiceMasterDTO();
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
        public async Task<List<InvoiceMasterDTO>> GetAllAsync()
        {


            List<InvoiceMasterDTO> invoiceMasterdtos = new List<InvoiceMasterDTO>();

            var invoiceMasters = await _repo.GetAllAsync();

            foreach (var invoiceMaster in invoiceMasters)
            {
                InvoiceMasterDTO invoiceMasterDTO = await ConvertInvoiceMasterToDTO(invoiceMaster);
                invoiceMasterdtos.Add(invoiceMasterDTO);


            }

            return invoiceMasterdtos;
        }
        public async Task<InvoiceMasterDTO?> GetByIdAsync(int id)
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

        public async Task<List<TripDashboardDTO>> GetAllInvoicDashboardListbyStatusAsync(int year)
        {

            DateTime today = DateTime.Today;
            DateTime lastWeekStart = today.AddDays(-7);
            DateTime prevWeekStart = today.AddDays(-14);
            DateTime prevWeekEnd = today.AddDays(-7);

            var trips =  _tripOrderRepository.QuerableTripListAsyc();
            if (year > 0)
            {
                trips = trips.Where(t => t.VehicleTakeOfTime.Year == year);
            }
            var UninvoicedTrips = await trips.Where(t => t.Status == "Completed" && t.IsInvoiced == false).CountAsync();





            var dashboard = new List<TripDashboardDTO>
            {
                  new TripDashboardDTO
        {
            Title = "UnInvoiced Trips",
            Value = UninvoicedTrips,
            Change = 0,
            Color = "#28A745",
            Route = "completed"
        },   
                new TripDashboardDTO
        {
            Title = "Pending Invoices",
            Value = UninvoicedTrips,
            Change = 0,
            Color = "#28A745",
            Route = "completed"
        },  new TripDashboardDTO
        {
            Title = "Completed Invoices",
            Value = UninvoicedTrips,
            Change = 0,
            Color = "#28A745",
            Route = "completed"
        },
            };

            return dashboard;

        }
    }
}