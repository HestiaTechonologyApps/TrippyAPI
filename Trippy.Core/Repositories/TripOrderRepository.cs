using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Core.Helpers;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class TripOrderRepository : GenericRepository<TripOrder>, ITripOrderRepository
    {
        private readonly AppDbContext _context;
        public TripOrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripOrderDTO>> GetCanceledTripsAsync()
        {

            return await _context.TripOrders
                .Where(t => t.TripStatus == "Canceled").Select(t => new TripOrderDTO 
            { 
                TripOrderId = t.TripOrderId,
                FromLocation = t.FromLocation,
                FromDate = t.FromDate, 
                TripStatus = t.TripStatus 
            }).ToListAsync();

        }

        public TripOrderDTO  GetTripDetails(int tripid)
        {
         return  ( from tripOrder in _context.TripOrders
                        join
                        cust in _context.Customers on tripOrder.CustomerId equals cust.CustomerId
                        join drv in _context.Drivers on tripOrder.DriverId equals drv.DriverId

                        where tripOrder.TripOrderId == tripid
                        select new TripOrderDTO
                        {
                            TripOrderId = tripOrder.TripOrderId,
                            TripBookingModeId = tripOrder.TripBookingModeId,
                            CustomerId = tripOrder.CustomerId,
                            DriverId = tripOrder.DriverId,
                            FromDate = tripOrder.FromDate,
                            ToDate = tripOrder.ToDate,
                            FromLocation = tripOrder.FromLocation,
                            ToLocation1 = tripOrder.ToLocation1,
                            ToLocation2 = tripOrder.ToLocation2,
                            ToLocation3 = tripOrder.ToLocation3,
                            ToLocation4 = tripOrder.ToLocation4,
                            PaymentMode = tripOrder.PaymentMode,
                            PaymentDetails = tripOrder.PaymentDetails,
                            BookedBy = tripOrder.BookedBy,
                            TripDetails = tripOrder.TripDetails,
                            TripStatus = tripOrder.TripStatus,
                            TripAmount = tripOrder.TripAmount,
                            AdvanceAmount = tripOrder.AdvanceAmount,
                            BalanceAmount = tripOrder.BalanceAmount,
                            IsActive = tripOrder.IsActive,
                            CustomerName = cust.CustomerName,
                            DriverName = drv.DriverName,

                        }).First();
           
        }


        public  IEnumerable<TripListDataDTO> GetTripListAsync()
        {
            return  (from tripOrder in _context.TripOrders
                    join
                    cust in _context.Customers on tripOrder.CustomerId equals cust.CustomerId
                    join drv in _context.Drivers on tripOrder.DriverId equals drv.DriverId

                    select new TripListDataDTO
                    {
                        TripOrderId = tripOrder.TripOrderId,
                        TripCode = "T-" + tripOrder.TripOrderId.ToString(),
                        FromDate=CustomDateHelper.ConvertToLocalTimeFormat(tripOrder.FromDate,""),
                        //FromDate = tripOrder.FromDate.HasValue ? tripOrder.FromDate.Value.ToString("dd MMMM yyyy hh:mm tt") : "",
                        IsActive = tripOrder.IsActive,
                        CustomerName = cust.CustomerName,
                        DriverName = drv.DriverName,
                        PickUpFrom = tripOrder.FromLocation,
                        RecivedVia = tripOrder.TripBookingModeId == 1 ? "Phone" :
                         tripOrder.TripBookingModeId == 2 ? "Direct Booking" :
                         tripOrder.TripBookingModeId == 3 ? "Website" :
                         "Unknown",
                        Status = tripOrder.TripStatus

                    }).ToList();

        }

        public async Task<IEnumerable<TripOrder>> GetAllByStatusAsync(string status)
        {
            return await _context.TripOrders
                .Where(t => t.TripStatus.ToLower() == status.ToLower())
                .ToListAsync();
        }

        public async Task<int> GetTotalTripsAsync()
        {
            return await _context.TripOrders.CountAsync();
        }



        public async Task<TripDashboardDTO> GetDashBoardData(string ststus)
        {
             var lst = new List<TripDashboardDTO>();

            TripDashboardDTO totaltripdtp= new TripDashboardDTO();
            totaltripdtp.Value = await GetTripCountByStatusAsync("All");
            totaltripdtp.Title = "Total Trips";
            lst.Add(totaltripdtp);

            TripDashboardDTO inprogresstripdtp = new TripDashboardDTO();
            totaltripdtp.Value = await GetTripCountByStatusAsync("All");
            totaltripdtp.Title = "Total Trips";
            lst.Add(totaltripdtp);


            DateTime today = DateTime.Today;

            TripDashboardDTO todaytripdtp = new TripDashboardDTO();
            todaytripdtp.Value = await _context.TripOrders
                .CountAsync(t => t.FromDate >= today && t.FromDate < today.AddDays(1));
            todaytripdtp.Title = "Today's Trips";
            todaytripdtp.Date = today;
            lst.Add(todaytripdtp);


            TripDashboardDTO canceltripdtp = new TripDashboardDTO();
            canceltripdtp.Value = await GetTripCountByStatusAsync("Canceled");
            canceltripdtp.Title = "Canceled Trips";
            lst.Add(canceltripdtp);

            TripDashboardDTO completedtripdtp = new TripDashboardDTO();
            completedtripdtp.Value = await GetTripCountByStatusAsync("Completed");
            completedtripdtp.Title = "Completed Trips";
            lst.Add(completedtripdtp);

          



            return lst.FirstOrDefault();
            
        }


        public async Task<int> GetTripCountByStatusAsync(string ststus)
        {
            
            return await _context.TripOrders.CountAsync(t => t.TripStatus.ToLower() == ststus.ToLower());
        }

        public async Task<int> GetTripCountByStatusAndDateRangeAsync(string status, DateTime startDate, DateTime endDate)
        {
            return await _context.TripOrders
                .Where(t => t.TripStatus.ToLower() == status.ToLower() &&
                            t.FromDate >= startDate &&
                            t.FromDate <= endDate)
                .CountAsync();
        }

        public async Task<int> GetTodaysTripsCountAsync(DateTime today)
        {
            var startOfDay = today.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _context.TripOrders
                .CountAsync(t => t.ToDate.HasValue &&
                                 t.ToDate.Value >= startOfDay &&
                                 t.FromDate.Value < endOfDay);
        }

        public async Task<List<TripOrder>> GetTodaysTripsAsync(DateTime today)
        {
            var startOfDay = today.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _context.TripOrders
                .Where(t => t.ToDate.HasValue &&
                            t.ToDate.Value >= startOfDay &&
                            t.FromDate.Value < endOfDay)
                .ToListAsync();
        }

        public async Task<List<TripOrder>> GetTripsByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _context.TripOrders
                .Where(t => t.ToDate.HasValue &&
                            t.ToDate.Value >= startOfDay &&
                            t.FromDate.Value < endOfDay)
                .ToListAsync();
        }
    }
    
}
