using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class TripOrderService : ITripOrderService
    {
        private readonly ITripOrderRepository _repo;
        private readonly IAuditRepository _auditRepository;

        public String AuditTableName { get; set; } = "TRIPORDER"; // THIS name eill be added everywhere to avoid spelling mistake
        public TripOrderService(ITripOrderRepository repo, IAuditRepository auditRepository)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
        }
        public async Task<TripOrderDTO> CreateAsync(TripOrder tripOrder)
        {
            await _repo.AddAsync(tripOrder);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<TripOrder>(
               tableName: AuditTableName,
               action: "create",
               recordId: tripOrder.TripOrderId,
               oldEntity: null,
               newEntity: tripOrder,
               changedBy: "System" // Replace with actual user info
           );
            return await ConvertTripOrderToDTO(tripOrder);
        }

        private async Task<TripOrderDTO> ConvertTripOrderToDTO(TripOrder tripOrder)
        {

            TripOrderDTO tripOrderDTO = new TripOrderDTO();
            tripOrderDTO.TripOrderId = tripOrder.TripOrderId;
            tripOrderDTO.TripBookingModeId = tripOrder.TripBookingModeId;
            tripOrderDTO.CustomerId = tripOrder.CustomerId;
            tripOrderDTO.DriverId = tripOrder.DriverId;
            tripOrderDTO.FromDate = tripOrder.FromDate;
            tripOrderDTO.FromDateString = tripOrder.FromDate.HasValue ? tripOrder.FromDate.Value.ToString("dd MMMM yyyy hh:mm tt") : "";
            tripOrderDTO.ToDate = tripOrder.ToDate;
            tripOrderDTO.ToDateString = tripOrder.ToDate.HasValue ? tripOrder.ToDate.Value.ToString("dd MMMM yyyy hh:mm tt") : "";
            tripOrderDTO.FromLocation = tripOrder.FromLocation;
            tripOrderDTO.ToLocation1 = tripOrder.ToLocation1;
            tripOrderDTO.ToLocation2 = tripOrder.ToLocation2;
            tripOrderDTO.ToLocation3 = tripOrder.ToLocation3;
            tripOrderDTO.ToLocation4 = tripOrder.ToLocation4;
            tripOrderDTO.PaymentMode = tripOrder.PaymentMode;
            tripOrderDTO.PaymentDetails = tripOrder.PaymentDetails;
            tripOrderDTO.BookedBy = tripOrder.BookedBy;
            tripOrderDTO.TripDetails = tripOrder.TripDetails;
            tripOrderDTO.TripStatus = tripOrder.TripStatus;
            tripOrderDTO.TripAmount = tripOrder.TripAmount;
            tripOrderDTO.AdvanceAmount = tripOrder.AdvanceAmount;
            tripOrderDTO.BalanceAmount = tripOrder.BalanceAmount;
            tripOrderDTO.IsActive = tripOrder.IsActive;
            tripOrderDTO.PaymentMode = tripOrder.PaymentMode;
            tripOrderDTO.PaymentDetails = tripOrder.PaymentDetails;


            tripOrderDTO.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync("TripOrder", tripOrder.TripOrderId);

            return tripOrderDTO;

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var tripOrder = await _repo.GetByIdAsync(id);
            if (tripOrder == null) return false;
            _repo.Delete(tripOrder);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<TripOrder>(
               tableName: AuditTableName,
               action: "Delete",
               recordId: tripOrder.TripOrderId,
               oldEntity: tripOrder,
               newEntity: tripOrder,
               changedBy: "System" // Replace with actual user info
           );
            return true;
        }
        public async Task<List<TripOrderDTO>> GetAllAsync()
        {


            List<TripOrderDTO> tripOrderdtos = new List<TripOrderDTO>();

            var tripOrders = await _repo.GetAllAsync();

            foreach (var tripOrder in tripOrders)
            {
                TripOrderDTO tripOrderDTO = await ConvertTripOrderToDTO(tripOrder);
                tripOrderdtos.Add(tripOrderDTO);


            }

            return tripOrderdtos;
        }


        public IEnumerable<TripListDataDTO> GetAll()
        {




            var tripOrders = _repo.GetTripListAsync();



            return tripOrders;
        }
        public async Task<TripOrderDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var tripOrderdto = await ConvertTripOrderToDTO(q);
            tripOrderdto.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync(AuditTableName, tripOrderdto.TripOrderId);

            return tripOrderdto;
        }


        public async Task<bool> UpdateAsync(TripOrder tripOrder)
        {
            var oldentity = await _repo.GetByIdAsync(tripOrder.TripOrderId);
            _repo.Detach(oldentity);
            _repo.Update(tripOrder);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<TripOrder>(
               tableName: AuditTableName,
               action: "update",
               recordId: tripOrder.TripOrderId,
               oldEntity: oldentity,
               newEntity: tripOrder,
               changedBy: "System" // Replace with actual user info
           );
            return true;
        }
    }
}