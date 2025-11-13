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


        private IQueryable<TripListDataDTO> QuerableTripListAsyc()
        {
            return from tripOrder in _context.TripOrders
                   join cust in _context.Customers on tripOrder.CustomerId equals cust.CustomerId
                   join drv in _context.Drivers on tripOrder.DriverId equals drv.DriverId
                   join trpmod in _context.TripBookingModes on tripOrder.TripBookingModeId
                   equals trpmod.TripBookingModeId
                   select new TripListDataDTO
                   {
                       TripOrderId = tripOrder.TripOrderId,
                       TripCode = tripOrder.TripCode,
                       FromDateString = tripOrder.FromDate.HasValue ? tripOrder.FromDate.Value.ToString("dd MMMM yyyy hh:mm tt") : "",
                       ToDateString = tripOrder.ToDate.HasValue ? tripOrder.ToDate.Value.ToString("dd MMMM yyyy hh:mm tt") : "",

                       FromDate = tripOrder.FromDate,
                      
                       ToDate = tripOrder.ToDate,
                       
                       //FromDate = CustomDateHelper.ConvertToLocalTimeFormat(tripOrder.FromDate, ""),
                       //ToDate = CustomDateHelper.ConvertToLocalTimeFormat(tripOrder.FromDate, ""),
                       IsActive = tripOrder.IsActive,

                       CustomerName = cust.CustomerName,
                       DriverName = drv.DriverName,
                       PickUpFrom = tripOrder.FromLocation,
                       RecivedVia = trpmod.TripBookingModeName,
                       PaymentMode = tripOrder.PaymentMode,

                       Status = tripOrder.TripStatus
                   };
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

        public TripOrderDTO GetTripDetails(int tripid)
        {
            return (from tripOrder in _context.TripOrders
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
                        FromDateString = tripOrder.FromDate.HasValue
                            ? tripOrder.FromDate.Value.ToString("dd MMMM yyyy hh:mm tt")
                            : string.Empty,
                        ToDate = tripOrder.ToDate,
                        ToDateString = tripOrder.ToDate.HasValue
                            ? tripOrder.ToDate.Value.ToString("dd MMMM yyyy hh:mm tt")
                            : string.Empty,
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



        public async Task<List<TripListDataDTO>> GetTripListAsync()
        {
            IQueryable<TripListDataDTO> q = QuerableTripListAsyc();

            return await q.ToListAsync();

        }






        public async Task<int> GetTotalTripsAsync()
        {
            return await _context.TripOrders.CountAsync();
        }



        public async Task<TripDashboardDTO> GetDashBoardData(string ststus)
        {
            var lst = new List<TripDashboardDTO>();

            TripDashboardDTO totaltripdtp = new TripDashboardDTO();
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

        public async Task<List<TripListDataDTO>> GetTodaysTripsAsync(DateTime today)
        {
            var startOfDay = today.Date;
            var endOfDay = startOfDay.AddDays(1);

            var q = QuerableTripListAsyc();

            q = q.Where(t => t.FromDate.HasValue &&
                    t.ToDate.HasValue &&
                    t.ToDate.Value >= startOfDay &&
                    t.FromDate.Value <= endOfDay);

            //q = q.Where(t =>
            //       t.FromDate != null &&
            //       t.ToDate != null &&
            //       DateTime.Parse(t.ToDate) >= startOfDay &&
            //       DateTime.Parse(t.FromDate) <= endOfDay
            //   );


            return await  q.ToListAsync();
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

        public async Task<List<TripListDataDTO>> GetAllByStatusAndYearAsync(string status, int year)
        {
            var q = QuerableTripListAsyc();
            if (!string.IsNullOrEmpty(status))
            {
                q = q.Where(t => t.Status.ToLower() == status.ToLower());
            }
            if (year > 0)
            {
                q = q.Where(t => t.FromDate != null &&
                                DateTime.Parse(t.FromDate.Value.ToString ()).Year == year);
            }

            return await q.ToListAsync();


        }





        public async Task<List<TripOrder>> GetAllTripsAsync()
        {
            return await _context.TripOrders
        .Include(t => t.CustomerId)
        .Include(t => t.DriverId)
        .ToListAsync();


        }

        public Task<List<TripOrderDTO>> GetAllTripsDetailAsync()
        {
            var result = (from trp in _context.TripOrders


                          join cust in _context.Customers
                              on trp.CustomerId equals cust.CustomerId
                          join drv in _context.Drivers
                              on trp.DriverId equals drv.DriverId
                          join trpmod in _context.TripBookingModes
                              on trp.TripBookingModeId equals trpmod.TripBookingModeId
                          select new TripOrderDTO
                          {
                              TripOrderId = trp.TripOrderId,
                              TripBookingModeId = trp.TripBookingModeId,

                              CustomerId = trp.CustomerId,
                              DriverId = trp.DriverId,

                              FromLocation = trp.FromLocation,
                              ToLocation1 = trp.ToLocation1,
                              ToLocation2 = trp.ToLocation2,
                              ToLocation3 = trp.ToLocation3,
                              ToLocation4 = trp.ToLocation4,
                              PaymentMode = trp.PaymentMode,
                              PaymentDetails = trp.PaymentDetails,
                              BookedBy = trp.BookedBy,
                              TripDetails = trp.TripDetails,
                              TripStatus = trp.TripStatus,
                              TripAmount = trp.TripAmount,
                              AdvanceAmount = trp.AdvanceAmount,
                              BalanceAmount = trp.BalanceAmount,
                              IsActive = trp.IsActive,
                              FromDate = trp.FromDate,
                              ToDate = trp.ToDate,

                              FromDateString = trp.FromDate.HasValue
                            ? trp.FromDate.Value.ToString("dd MMMM yyyy hh:mm tt")
                            : string.Empty,

                              ToDateString = trp.ToDate.HasValue
                            ? trp.ToDate.Value.ToString("dd MMMM yyyy hh:mm tt")
                            : string.Empty,
                              TripBookingModeName = trpmod.TripBookingModeName,

                              CustomerName = cust.CustomerName,
                              DriverName = drv.DriverName
                          }).ToListAsync();
            return result;
        }


    }


}




