using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class VehicleMaintenanceRecordDTO
    {
        public int VehicleMaintenanceRecordId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = "";
        public DateTime MaintenanceDate { get; set; }
        public string MaintenanceDateString => MaintenanceDate.ToString("dd MMM yyyy hh:mm tt");
        public string MaintenanceType { get; set; } = string.Empty; // Preventive / Corrective / Accident Repair
        public string WorkshopName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int OdometerReading { get; set; }
        public string PerformedBy { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedDateString => CreatedDate.ToString("dd MMM yyyy hh:mm tt");
        public string CreatedBy { get; set; } = string.Empty;
        public int CompanyId { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public List<AuditLogDTO> AudiitLgs { get; set; } = new List<AuditLogDTO>();
    }
}
