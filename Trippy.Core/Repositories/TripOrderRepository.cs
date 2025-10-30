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


    }
}
