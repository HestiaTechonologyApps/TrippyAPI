using Microsoft.AspNetCore.Mvc;
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



        public IQueryable<TripListDataDTO> QuerableTripListAsyc()
        {
            return from tripOrder in _context.TripOrders.AsNoTracking()
                   join cust in _context.Customers.AsNoTracking() on tripOrder.CustomerId equals cust.CustomerId
                   join drv in _context.Drivers.AsNoTracking() on tripOrder.DriverId equals drv.DriverId
                   join trpmod in _context.TripBookingModes.AsNoTracking()
                        on tripOrder.TripBookingModeId equals trpmod.TripBookingModeId
                   where tripOrder.IsDeleted == false
                   select new TripListDataDTO
                   {
                       TripOrderId = tripOrder.TripOrderId,
                       CustomerId = tripOrder.CustomerId,
                       TripCode = tripOrder.TripCode,
                       FromDate = tripOrder.FromDate,
                       ToDate = tripOrder.ToDate,
                       IsActive = tripOrder.IsActive,
                       CustomerName = cust.CustomerName ?? "",
                       DriverName = drv.DriverName ?? "",
                       PickUpFrom = tripOrder.FromLocation ?? "",
                       RecivedVia = trpmod.TripBookingModeName ?? "",
                       PaymentMode = tripOrder.PaymentMode ?? "",
                       Status = tripOrder.TripStatus ?? "",
                       IsInvoiced = tripOrder.IsInvoiced,
                       VehicleTakeOfTime = tripOrder.VehicleTakeOfTime,
                       DepartmentName = tripOrder.DepartmentName,
                       ToLocation1 = tripOrder.ToLocation1,
                       ToLocation2 = tripOrder.ToLocation2,
                       ToLocation3 = tripOrder.ToLocation3,
                       ToLocation4 = tripOrder.ToLocation4


                   };
        }

        //public IQueryable<TripListDataDTO> QuerableTripListAsyc()
        //{
        //    return from tripOrder in _context.TripOrders
        //           join cust in _context.Customers on tripOrder.CustomerId equals cust.CustomerId
        //           join drv in _context.Drivers on tripOrder.DriverId equals drv.DriverId
        //           join trpmod in _context.TripBookingModes on tripOrder.TripBookingModeId
        //           equals trpmod.TripBookingModeId
        //           select new TripListDataDTO
        //           {
        //               TripOrderId = tripOrder.TripOrderId,
        //               TripCode = tripOrder.TripCode,
        //               FromDateString = tripOrder.FromDate.HasValue ? tripOrder.FromDate.Value.ToString("dd MMMM yyyy hh:mm tt") : "",
        //               ToDateString = tripOrder.ToDate.HasValue ? tripOrder.ToDate.Value.ToString("dd MMMM yyyy hh:mm tt") : "",

        //               FromDate = tripOrder.FromDate,

        //               ToDate = tripOrder.ToDate,

        //               //FromDate = CustomDateHelper.ConvertToLocalTimeFormat(tripOrder.FromDate, ""),
        //               //ToDate = CustomDateHelper.ConvertToLocalTimeFormat(tripOrder.FromDate, ""),
        //               IsActive = tripOrder.IsActive,

        //               CustomerName = cust.CustomerName,
        //               DriverName = drv.DriverName,
        //               PickUpFrom = tripOrder.FromLocation,
        //               RecivedVia = trpmod.TripBookingModeName,
        //               PaymentMode = tripOrder.PaymentMode,

        //               Status = tripOrder.TripStatus
        //           };
        //}





        public async Task<IEnumerable<TripListDataDTO>> GetCanceledTripsAsync()
        {



            var q = await QuerableTripListAsyc().Where(t => t.Status == "Canceled").ToListAsync();

            return q;

        }

        public async Task<TripOrderDTO> GetTripDetails(int tripid)
        {


            var q = (from tripOrder in _context.TripOrders
                     join
                     cust in _context.Customers on tripOrder.CustomerId equals cust.CustomerId
                     join drv in _context.Drivers on tripOrder.DriverId equals drv.DriverId
                     join trmp in _context.TripBookingModes on tripOrder.TripBookingModeId equals trmp.TripBookingModeId

                     where tripOrder.TripOrderId == tripid
                     select new TripOrderDTO
                     {
                         TripOrderId = tripOrder.TripOrderId,
                         TripCode = tripOrder.TripCode,

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
                         TripBookingModeName = trmp.TripBookingModeName,
                         VehicleTakeOfTime = tripOrder.VehicleTakeOfTime,
                         DepartmentName = tripOrder.DepartmentName,
                         AdvanceAmount = tripOrder.AdvanceAmount,
                         BalanceAmount = tripOrder.BalanceAmount,
                         IsActive = tripOrder.IsActive,
                         CustomerName = cust.CustomerName,
                         DriverName = drv.DriverName,
                         IsDeleted = tripOrder.IsDeleted
                         
                         

                     }).FirstAsync();
            return await q;

        }



        public async Task<List<TripListDataDTO>> GetTripListAsync()
        {
            IQueryable<TripListDataDTO> q = QuerableTripListAsyc();

            return await q.ToListAsync();

        }






        public async Task<int> GetTotalTripsAsync(int year)
        {
            return await QuerableTripListAsyc().Where(t => t.FromDate != null &&
                                t.FromDate.Value.Year == year).CountAsync();

        }



    
        public async Task<int> GetTripCountByStatusAsync(int CompanyId, int year, string ststus)
        {
            var q = QuerableTripListAsyc();
            if (CompanyId != 0)
            {
                //  q=q.Where (t=>t.CompanyId==CompanyId)
            }
            q = q.Where(t => t.FromDate != null &&
                                t.FromDate.Value.Year == year);

            return await q.CountAsync(t => t.Status.ToLower() == ststus.ToLower());

            //return await _context.TripOrders.CountAsync(t => t.TripStatus.ToLower() == ststus.ToLower());
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

            var q = QuerableTripListAsyc();

            return await q.CountAsync(t =>
        t.FromDate.HasValue &&
        t.FromDate.Value.Date == today.Date
    );


        }

        public async Task<List<TripListDataDTO>> GetTodaysTripsAsync(DateTime today)
        {
            var startOfDay = today.Date;
            var endOfDay = startOfDay.AddDays(1);

            var q = QuerableTripListAsyc();

            q = q.Where(t => t.FromDate.HasValue &&
                 t.FromDate.Value.Date == today.Date);

            return await q.ToListAsync();
        }

        public async Task<List<TripListDataDTO>> GetTripsByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);
            var q = QuerableTripListAsyc();

            q = q.Where(t => t.ToDate.HasValue &&
                            t.ToDate.Value >= startOfDay &&
                            t.FromDate.Value < endOfDay);
            return await q.ToListAsync();

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
                                t.FromDate.Value.Year == year);
            }

            return await q.ToListAsync();


        }





        public async Task<List<TripListDataDTO>> GetAllTripsAsync()
        {
            return await QuerableTripListAsyc().ToListAsync();


        }

        public async Task<List<CalendarEventDto>> GetDriverSchedule(int driverId, DateTime start, DateTime end)
        {
            // 1. Validate Date Range
            if (start == default || end == default)
            {
                // Default to current month if not provided
                var now = DateTime.UtcNow;
                start = new DateTime(now.Year, now.Month, 1);
                end = start.AddMonths(1).AddDays(-1);
            }

            // 2. Query Database
            // We look for trips that overlap with the requested window
            var trips = await _context.TripOrders
                .Where(t =>
                    t.DriverId == driverId &&
                    !t.IsDeleted &&
                    t.IsActive &&
                    t.TripStatus != "Cancelled" &&
                    t.FromDate.HasValue &&
                    t.ToDate.HasValue &&
                    // Overlap logic: Trip starts before window ends AND Trip ends after window starts
                    t.FromDate < end &&
                    t.ToDate > start
                )
                .Select(t => new
                {
                    t.TripOrderId,
                    t.TripCode,
                    t.FromLocation,
                    t.ToLocation1,
                    t.FromDate,
                    t.ToDate,
                    t.TripStatus
                    // t.CustomerName // Assuming you joined Customer table or have this field
                })
                .ToListAsync();

            // 3. Map to Calendar Event DTO
            var events = trips.Select(t => new CalendarEventDto
            {
                Id = t.TripOrderId,
                Title = $"{t.TripCode} - {t.FromLocation} to {t.ToLocation1}",
                Start = t.FromDate.Value,
                End = t.ToDate.Value,
                Status = t.TripStatus,
                Description = $"Status: {t.TripStatus}"
            });

            return events.ToList();



        }
        public async Task<int> GetUpcomingTripsUsingTakeOffTimeAsync(int ComanyId, int year, string ststus)
        {
            DateTime now = DateTime.UtcNow;
            DateTime startTime = now.AddHours(-18);   // past 3 hours
            DateTime endTime = now.AddHours(24);     // next 24 hours

            return await _context.TripOrders
                .Where(t => t.TripStatus.ToLower() == "scheduled" &&
                           t.VehicleTakeOfTime >= startTime &&
                t.VehicleTakeOfTime <= endTime)
                .CountAsync();
        }



        public async Task<List<AuditLogDTO>> GetAuditLogNotifications(int CompanyId, string tablename = "")
        {
            var oneDayAgo = DateTime.UtcNow.AddDays(-1);

            var audits = await (
                from audit in _context.AuditLogs
                join trip in _context.TripOrders
                    on audit.RecordID equals trip.TripOrderId
                where audit.TableName == tablename
                      && trip.CompanyId == CompanyId
                      && audit.ChangedAt >= oneDayAgo
                select new AuditLogDTO
                {
                    LogID = audit.LogID,
                    TableName = audit.TableName,
                    Action = audit.Action,
                    RecordID = audit.RecordID,
                    ChangedBy = audit.ChangedBy,
                    ChangedAt = audit.ChangedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    ChangeDetails = trip.TripCode
                }
            ).ToListAsync();

            return audits;
        }


    }
}



