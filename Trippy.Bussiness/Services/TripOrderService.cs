﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trippy.Core.Repositories;
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


            //tripOrderDTO.AuditLogs = await _auditRepository.GetAuditLogsForEntityAsync("TripOrder", tripOrder.TripOrderId);

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

        public async Task<List<TripOrderDTO>> GetAllTripListbyStatusAsync(string Status)
        {
            if (string.IsNullOrWhiteSpace(Status))
                return new List<TripOrderDTO>();


            var tripOrders = await _repo.GetAllByStatusAsync(Status);


            var tripOrderDtos = new List<TripOrderDTO>();

            foreach (var tripOrder in tripOrders)
            {
                var dto = await ConvertTripOrderToDTO(tripOrder);
                tripOrderDtos.Add(dto);
            }

            return tripOrderDtos;
        }

        public async Task<IEnumerable<TripOrderDTO>> GetCanceledTripsAsync()
        {
            return await _repo.GetCanceledTripsAsync();
        }

        public async Task<int> GetTodaysTripCountAsync()
        {
            var today = DateTime.Today;
            return await _repo.GetTodaysTripsCountAsync(today);
        }


        public async Task<List<TripDashboardDTO>> GetAllTripDashboardListbyStatusAsync()
        {
            DateTime today = DateTime.Today;
            DateTime lastWeekStart = today.AddDays(-7);
            DateTime prevWeekStart = today.AddDays(-14);
            DateTime prevWeekEnd = today.AddDays(-7);

          
            int totalTrips = await _repo.GetTotalTripsAsync();
            int todaysTrips = await _repo.GetTodaysTripsCountAsync(today);
            int cancelled = await _repo.GetTripCountByStatusAsync("Canceled");
            int completed = await _repo.GetTripCountByStatusAsync("Completed");
            int scheduled = await _repo.GetTripCountByStatusAsync("Scheduled");

         
            int prevCancelled = await _repo.GetTripCountByStatusAndDateRangeAsync("Canceled", prevWeekStart, prevWeekEnd);
            int prevCompleted = await _repo.GetTripCountByStatusAndDateRangeAsync("Completed", prevWeekStart, prevWeekEnd);
            int prevTodaysTrip = await _repo.GetTripCountByStatusAndDateRangeAsync("Todays Trip", prevWeekStart, prevWeekEnd);
            int prevScheduled = await _repo.GetTripCountByStatusAndDateRangeAsync("Scheduled", prevWeekStart, prevWeekEnd);
            int prevTotal = totalTrips - 20; 

           
            int changeTotal = totalTrips - prevTotal;
            int changeTodayTrip = todaysTrips - prevTodaysTrip;
            int changeCancelled = cancelled - prevCancelled;
            int changeCompleted = completed - prevCompleted;
            int changeScheduled = scheduled - prevScheduled;

           
            var dashboard = new List<TripDashboardDTO>
    {
        new TripDashboardDTO
        {
            Title = "Total Trips",
            Value = totalTrips,
            Change = changeTotal,
            Color = "#000",
            Route = "total-trips"
        },
        new TripDashboardDTO
        {
            Title = "Today's Trips",
            Value = todaysTrips,
            Change = changeTodayTrip,
            Color = "#9F9FF8",
            Route = "today-trips"
        },
        new TripDashboardDTO
        {
            Title = "Cancelled",
            Value = cancelled,
            Change = changeCancelled,
            Color = "#FF2A2A",
            Route = "bookings"
        },
        new TripDashboardDTO
        {
            Title = "Completed",
            Value = completed,
            Change = changeCompleted,
            Color = "#28A745",
            Route = "completed"
        },
        new TripDashboardDTO
        {
            Title = "Scheduled",
            Value = scheduled,
            Change = changeScheduled,
            Color = "#9F9FF8",
            Route = "scheduled"
        }
    };

            return dashboard;
        }

      
    }
        }

    
