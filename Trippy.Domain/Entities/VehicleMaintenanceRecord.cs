using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class VehicleMaintenanceRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleMaintenanceRecordId { get; set; }
        public int VehicleId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string MaintenanceType { get; set; } = string.Empty; // Preventive / Corrective / Accident Repair
        public string WorkshopName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int OdometerReading { get; set; }
        public string PerformedBy { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public int CompanyId { get; set; } = 0;
    }



}
