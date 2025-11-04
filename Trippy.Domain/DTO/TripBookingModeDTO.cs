using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class TripBookingModeDTO
    {
        public int TripBookingModeId { get; set; } // Primary key
        public String TripBookingModeName { get; set; } = "";
        public string TripModeName { get; set; } = "";
    }
}
