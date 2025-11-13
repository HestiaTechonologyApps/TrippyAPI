using Trippy.Domain.Entities;

namespace Trippy.Domain.DTO
{
    public class TripOrderDTO
    {

        public int TripOrderId { get; set; } // Primary key
        public int TripBookingModeId { get; set; }
        public string TripCode { get; set; } = "";
        public string TripBookingModeName { get; set; } = "";

        public int CustomerId { get; set; }
        
        public int DriverId { get; set; }
       
        public DateTime? FromDate { get; set; }
        public string FromDateString { get; set; } = "";
        public DateTime? ToDate { get; set; }
        public string ToDateString { get; set; } = "";

        public string FromLocation { get; set; } = "";

        public string ToLocation1 { get; set; } = "";
        public string ToLocation2 { get; set; } = "";
        public string ToLocation3 { get; set; } = "";
        public string ToLocation4 { get; set; } = "";

        public string BookedBy { get; set; } = "";
        public string TripDetails { get; set; } = "";



        public string TripStatus { get; set; } = "";// Planned, InProgress, Completed, Cancelled
        public decimal TripAmount { get; set; } = 0;
        public decimal AdvanceAmount { get; set; } = 0;
        public decimal BalanceAmount { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        public string PaymentMode { get; set; } = "";
        public string PaymentDetails { get; set; } = "";
        public string TripModeName { get; set; } = "";
        
        public string CustomerName { get; set; } = "";

        public string DriverName { get; set; } = "";

        public List<AuditLogDTO> AuditLogs { get; set; } = new List<AuditLogDTO>();


    }

    public class TripListDataDTO
    {
        public int TripOrderId { get; set; }

        public string TripCode { get; set; } = "";
        public string FromDate { get; set; }
        public string FromDateString { get; set; }
        public string ToDate { get; set; }
        public string ToDateString { get; set; }
        public string CustomerName { get; set; }
        public string RecivedVia { get; set; }
        public string DriverName { get; set; }
        public string PickUpFrom { get; set; }
        public string Status { get; set; }
        public string AddedBy { get; set; }
        public string PaymentMode { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}

