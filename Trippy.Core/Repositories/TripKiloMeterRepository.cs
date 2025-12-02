using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class TripKiloMeterRepository : GenericRepository<TripKiloMeter>, ITripKiloMeterRepository
    {
        private readonly AppDbContext _context;
        public TripKiloMeterRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<TripKiloMeterDTO> QuerableTripKiloMeters()
        {
            var data =
                from km in _context.TripKiloMeters.AsNoTracking()
                join trip in _context.TripOrders.AsNoTracking() on km.TripOrderId equals trip.TripOrderId into tripGroup
                from trip in tripGroup.DefaultIfEmpty()
                join drv in _context.Drivers.AsNoTracking() on km.DriverId equals drv.DriverId into drvGroup
                from drv in drvGroup.DefaultIfEmpty()
                join veh in _context.Vehicles.AsNoTracking() on km.VehicleId equals veh.VehicleId into vehGroup
                from veh in vehGroup.DefaultIfEmpty()
                select new TripKiloMeterDTO
                {
                    TripKiloMeterId = km.TripKiloMeterId,
                    TripOrderId = km.TripOrderId,
                    DriverId = km.DriverId,
                    VehicleId = km.VehicleId,
                    TripStartTime = km.TripStartTime,
                    TripEndTime = km.TripEndTime,
                   
                    WaitingHours = km.WaitingHours,
                    TripStartReading = km.TripStartReading,
                    TripEndReading = km.TripEndReading,
                    TotalKM = km.TripEndReading - km.TripStartReading,
                    CreatedOn = km.CreatedOn,
                    
                    DriverName = drv.DriverName,
                    VehicleName = veh != null
                        ? veh.VehicleType + " - " + veh.RegistrationNumber
                        : "N/A"
                };

            return data.AsQueryable();
        }

        public async Task<List<TripKiloMeterDTO>> GetAllTripKilometers()
        {
            var data = await QuerableTripKiloMeters().ToListAsync();

            return data;
        }

        public TripKiloMeterDTO GetTripKilometerById(int id)
        {
            
            var data=QuerableTripKiloMeters().FirstOrDefault(km => km.TripKiloMeterId == id);

            return data;
        }
    }
}
