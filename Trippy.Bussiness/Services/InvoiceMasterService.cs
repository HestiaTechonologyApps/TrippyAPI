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


        private const int CurrentFiancialYearId= 2025;
        private const int CategoryID= 1;


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

        public async Task<CustomApiResponse> GenerateInvoice(InvoiceMasterIdList invoiceMasterIdList)
        {
            CustomApiResponse customApiResponse = new CustomApiResponse();
            String ErrorMessage = "";

            InvoiceMasterDTO invoiceMasterDTO = new InvoiceMasterDTO();
            invoiceMasterDTO.CompanyId = invoiceMasterIdList.CustomerId;
            invoiceMasterDTO.FinancialYearId = CurrentFiancialYearId;
            invoiceMasterDTO.InvoiceDate = DateTime.UtcNow.Date;
            invoiceMasterDTO.CreatedOn = DateTime.UtcNow;
            invoiceMasterDTO.IsDeleted = false;
            invoiceMasterDTO.IsCompleted = false;          


            foreach (var tripidid in invoiceMasterIdList.TripOrderIds ) {           
                
                var triporder = await _tripOrderRepository.GetByIdAsync(tripidid);

                if(triporder == null)
                {
                    ErrorMessage= $"Trip Order Id {tripidid} not found.";
                   
                }
                else
                {
                    if (triporder.IsInvoiced == true)
                    {
                        ErrorMessage += $"Trip Order Id {triporder.TripCode} is already invoiced.";

                    }
                    if (triporder.TripStatus != "Completed")
                    {
                        ErrorMessage += $"Trip Order Id {triporder.TripCode} is not completed yet.";
                    }
                    if(triporder.CustomerId != invoiceMasterIdList.CustomerId)
                    {
                        ErrorMessage += $"Trip Order Id {triporder.TripCode} does not belong to the selected customer.";
                    }
                    if (triporder.IsDeleted==true  )
                    {
                        ErrorMessage+= $"Trip Order Id {triporder.TripCode} is deleted.";
                    }
                }

                if(ErrorMessage.Trim() != "")
                {
                    customApiResponse.IsSucess = false;
                    customApiResponse.Error = ErrorMessage;                    
                }
                else
                {
                    invoiceMasterDTO.InvoiceDetailDtos = new List<InvoiceDetailDTO>();

                    InvoiceDetailDTO invoiceDetailDTO = new InvoiceDetailDTO();
                    invoiceDetailDTO.TripOrderId = tripidid;
                    invoiceDetailDTO.Ammount = triporder.TripAmount;
                    invoiceDetailDTO.CategoryId = CategoryID;
                    invoiceDetailDTO.TotalTax= 0;
                    invoiceDetailDTO.Discount= 0;
                    invoiceMasterDTO.InvoiceDetailDtos.Add(invoiceDetailDTO);
                }

            }


           if (ErrorMessage.Trim() != "")
            {
                customApiResponse.IsSucess = false;
                customApiResponse.Error = ErrorMessage;
            }
            else
            {
                customApiResponse.IsSucess = true;
                customApiResponse.Error = "";
            }
               


            return customApiResponse;
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
            var UninvoicedTrips = await trips.Where(t => t.Status == "Completed" && t.IsInvoiced == false && t.VehicleTakeOfTime.Year==year ).CountAsync();


            var newinvoices = await _repo.QuerableInvoiceMasterListAsyc().Where(i => i.CreatedOn.Year == year && i.IsDeleted == false && i.IsCompleted == false).CountAsync();

            var completedinvoices = await _repo.QuerableInvoiceMasterListAsyc().Where(i => i.CreatedOn.Year == year && i.IsDeleted == false && i.IsCompleted == true).CountAsync();

            var deletedinvoices = await _repo.QuerableInvoiceMasterListAsyc().Where(i => i.CreatedOn.Year == year && i.IsDeleted == true).CountAsync();


            var dashboard = new List<TripDashboardDTO>
            {
                  new TripDashboardDTO
        {
            Title = "UnInvoiced Trips",
            Value = UninvoicedTrips,
            Change = 0,
            Color = "#F8A23A",
            Route = "uninvoiced-trips"
        },   
                new TripDashboardDTO
        {
            Title = "Pending Invoices",
            Value = newinvoices,
            Change = 0,
            Color = "#FACC15",
            Route = "pending-invoices"
        },  new TripDashboardDTO
        {
            Title = "Completed Invoices",
            Value = completedinvoices,
            Change = 0,
            Color = "#28A745",
            Route = "completed-invoices"
        },
            new TripDashboardDTO
        {
            Title = "Canceled Invoices",
            Value = deletedinvoices,
            Change = 0,
            Color = "#FF2A2A",
            Route = "canceled-invoices"
        },

            };

            return dashboard;

        }
    }
}