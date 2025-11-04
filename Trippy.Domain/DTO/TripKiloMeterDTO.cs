using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class TripKiloMeterDTO
    {
        public int TripKiloMeterId { get; set; } // Primary key

        public int TripOrderId { get; set; } // Primary key
        public int DriverId { get; set; } // Primary key
        public string DriverName { get; set; } = "";
        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = "";
        public DateTime? TripStartTime { get; set; }
        public string TripStartTimeString { get; set; } = "";
        public DateTime? TripEndTime { get; set; }
        public string TripEndingTimeString { get; set; } = "";
        public int TripStartReading { get; set; }
        public int TripEndReading { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; } = "";

    }
}
