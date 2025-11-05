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

        public List<TripKiloMeterDTO> GetAllTripKilometers()
        {
            var data = (from km in _context.TripKiloMeters
                        join trip in _context.TripOrders on km.TripOrderId equals trip.TripOrderId into tripGroup
                        from trip in tripGroup.DefaultIfEmpty()
                        join drv in _context.Drivers on km.DriverId equals drv.DriverId into drvGroup
                        from drv in drvGroup.DefaultIfEmpty()
                        join veh in _context.Vehicles on km.VehicleId equals veh.VehicleId into vehGroup
                        from veh in vehGroup.DefaultIfEmpty()
                        select new TripKiloMeterDTO

                        {
                            TripKiloMeterId = km.TripKiloMeterId,
                            TripOrderId = km.TripOrderId,
                            DriverId = km.DriverId,
                            VehicleId = km.VehicleId,
                            TripStartTime = km.TripStartTime,
                            TripEndTime = km.TripEndTime,
                            TripStartTimeString = km.TripStartTime.HasValue
                                ? km.TripStartTime.Value.ToString("dd MMM yyyy hh:mm tt")
                                : string.Empty,
                            TripEndingTimeString = km.TripEndTime.HasValue
                                ? km.TripEndTime.Value.ToString("dd MMM yyyy hh:mm tt")
                                : string.Empty,

                            TripStartReading = km.TripStartReading,
                            TripEndReading = km.TripEndReading,
                            TotalKM = (km.TripStartReading ) + (km.TripEndReading ),
                            CreatedOn = km.CreatedOn,
                            CreatedOnString = km.CreatedOn.ToString("dd MMM yyyy hh:mm tt"),

                            DriverName = drv.DriverName,
                            VehicleName = veh != null ? veh.VehicleType + " - " + veh.RegistrationNumber : "N/A",

                        }).ToList();

            return data;
        }

        public TripKiloMeterDTO GetTripKilometerById(int id)
        {
            var data = (from km in _context.TripKiloMeters
                        join trip in _context.TripOrders on km.TripOrderId equals trip.TripOrderId into tripGroup
                        from trip in tripGroup.DefaultIfEmpty()
                        join drv in _context.Drivers on km.DriverId equals drv.DriverId into drvGroup
                        from drv in drvGroup.DefaultIfEmpty()
                        join veh in _context.Vehicles on km.VehicleId equals veh.VehicleId into vehGroup
                        from veh in vehGroup.DefaultIfEmpty()
                        where km.TripKiloMeterId == id
                        select new TripKiloMeterDTO
                        {
                            TripKiloMeterId = km.TripKiloMeterId,
                            TripOrderId = km.TripOrderId,
                            DriverId = km.DriverId,
                            VehicleId = km.VehicleId,
                            TripStartTime = km.TripStartTime,
                            TripEndTime = km.TripEndTime,
                            TripStartTimeString = km.TripStartTime.HasValue
                                ? km.TripStartTime.Value.ToString("dd MMM yyyy hh:mm tt")
                                : string.Empty,
                            TripEndingTimeString = km.TripEndTime.HasValue
                                ? km.TripEndTime.Value.ToString("dd MMM yyyy hh:mm tt")
                                : string.Empty,
                            TripStartReading = km.TripStartReading,
                            TripEndReading = km.TripEndReading,
                            TotalKM = (km.TripEndReading + km.TripStartReading),
                            CreatedOn = km.CreatedOn,
                            CreatedOnString = km.CreatedOn.ToString("dd MMM yyyy hh:mm tt"),
                            DriverName = drv.DriverName,
                            VehicleName = veh != null
                                ? veh.VehicleType + " - " + veh.RegistrationNumber
                                : "N/A",
                        }).FirstOrDefault();

            return data;
        }
    }
}
