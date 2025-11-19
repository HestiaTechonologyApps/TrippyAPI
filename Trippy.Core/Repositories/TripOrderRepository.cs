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
                       TripCode = tripOrder.TripCode,
                       FromDateString = tripOrder.FromDate.HasValue
                                        ? tripOrder.FromDate.Value.ToString("dd MMMM yyyy hh:mm tt")
                                        : "",
                       ToDateString = tripOrder.ToDate.HasValue
                                      ? tripOrder.ToDate.Value.ToString("dd MMMM yyyy hh:mm tt")
                                      : "",
                       FromDate = tripOrder.FromDate,
                       ToDate = tripOrder.ToDate,
                       IsActive = tripOrder.IsActive,
                       CustomerName = cust.CustomerName,
                       DriverName = drv.DriverName,
                       PickUpFrom = tripOrder.FromLocation,
                       RecivedVia = trpmod.TripBookingModeName,
                       PaymentMode = tripOrder.PaymentMode,
                       Status = tripOrder.TripStatus
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

                         FromDateString = tripOrder.FromDate.HasValue
                             ? tripOrder.FromDate.Value.ToString("dd MMMM yyyy hh:mm tt")
                             : string.Empty,

                         ToDateString = tripOrder.ToDate.HasValue
                             ? tripOrder.ToDate.Value.ToString("dd MMMM yyyy hh:mm tt")
                             : string.Empty,

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
                         IsDeleted = tripOrder.IsDeleted

                     }).FirstAsync();
            return await q;

        }



        public async Task<List<TripListDataDTO>> GetTripListAsync()
        {
            IQueryable<TripListDataDTO> q = QuerableTripListAsyc();

            return await q.ToListAsync();

        }






        public async Task<int> GetTotalTripsAsync()
        {
            return await QuerableTripListAsyc().CountAsync();
          //  return await _context.TripOrders.CountAsync();
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

            return await QuerableTripListAsyc().CountAsync(t => t.Status.ToLower() == ststus.ToLower());

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

            return await q.CountAsync(t => t.FromDate.HasValue &&
                    t.ToDate.HasValue &&
                    t.ToDate.Value >= startOfDay &&
                    t.FromDate.Value <= endOfDay);
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
                                DateTime.Parse(t.FromDate.Value.ToString()).Year == year);
            }

            return await q.ToListAsync();


        }





        public async Task<List<TripListDataDTO>> GetAllTripsAsync()
        {
            return await QuerableTripListAsyc().ToListAsync();


        }

      


    }


}




