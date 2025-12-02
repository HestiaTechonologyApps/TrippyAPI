using Trippy.Domain.Entities;

namespace Trippy.Domain.DTO
{

    public class TripStatusUpdateDTO
    {
        public int TripOrderId { get; set; } // Primary key
        public string TripStatus { get; set; } = "";
        public string Remark { get; set; } = "";


    }
        public class TripOrderDTO
    {

        public int TripOrderId { get; set; } // Primary key
        public int TripBookingModeId { get; set; }
        public string TripCode { get; set; } = "";
        public string TripBookingModeName { get; set; } = "";
        public string DepartmentName { get; set; } = "";

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

        public int CompanyId { get; set; } = 0;
        public string ComapanyName = "";

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
        public bool IsDeleted { get; set; } = false;
        public bool IsInvoiced { get; set; } = false;
        public DateTime VehicleTakeOfTime { get; set; }
        public string VehicleTakeOfTimeString => VehicleTakeOfTime.ToString("dd MMMM yyyy hh:mm tt") ?? "";

        public List<AuditLogDTO> AuditLogs { get; set; } = new List<AuditLogDTO>();


    }

    public class TripListDataDTO
    {
        public int TripOrderId { get; set; }
        public int CustomerId { get; set; }

        public string TripCode { get; set; } = "";
        public string ToLocation1 { get; set; } = "";
        public string ToLocation2 { get; set; } = "";
        public string ToLocation3 { get; set; } = "";
        public string ToLocation4 { get; set; } = "";
        public DateTime? FromDate { get; set; }
        public DateTime VehicleTakeOfTime { get; set; }

        public string FromDateString => FromDate?.ToString("dd MMMM yyyy hh:mm tt") ?? "";
        public string ToDateString => ToDate?.ToString("dd MMMM yyyy hh:mm tt") ?? "";
        public string VehicleTakeOfTimeString => VehicleTakeOfTime.ToString("dd MMMM yyyy hh:mm tt") ?? "";


        public string CommaSeperatedToLocations
        {
            get
            {
                List<string> locations = new List<string>();
                if (!string.IsNullOrWhiteSpace(ToLocation1)) locations.Add(ToLocation1);
                if (!string.IsNullOrWhiteSpace(ToLocation2)) locations.Add(ToLocation2);
                if (!string.IsNullOrWhiteSpace(ToLocation3)) locations.Add(ToLocation3);
                if (!string.IsNullOrWhiteSpace(ToLocation4)) locations.Add(ToLocation4);
                return string.Join(", ", locations);
            }
        }
      
        public DateTime? ToDate { get; set; }
        public string CustomerName { get; set; }
        public string RecivedVia { get; set; }
        public string DriverName { get; set; }
        public string PickUpFrom { get; set; }
        public string Status { get; set; }
        public string AddedBy { get; set; }
        public string PaymentMode { get; set; }
        public string DepartmentName { get; set; } = "";

        public bool IsDeleted { get; set; } = false;
        public bool IsInvoiced { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } // e.g., "Trip #123 - Customer Name"
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Status { get; set; } // To determine color (e.g., Planned, Completed)
        public string Description { get; set; } // Tooltip info
        public bool AllDay { get; set; } = false;
    }
}

