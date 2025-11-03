using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string ChassisNumber { get; set; } = string.Empty;
        public string EngineNumber { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty; // e.g. Truck, Bus, Pickup
        public DateTime RegistrationExpiry { get; set; }

        public string RegistrationExpiryString { get; set; } = "";
        public string CurrentStatus { get; set; } = "Active"; // Active / Inactive / UnderMaintenance
        public string Location { get; set; } = string.Empty; // Base depot / branch
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedDateString = "";
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedDateString = "";
        public string? UpdatedBy { get; set; }

        public List<AuditLogDTO> AuuditLgs { get; set; } = new List<AuditLogDTO>();
    }
}
