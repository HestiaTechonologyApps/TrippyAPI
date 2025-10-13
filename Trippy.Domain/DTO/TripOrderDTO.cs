namespace Trippy.Domain.DTO
{
    public class TripOrderDTO
    {

        public int TripOrderId { get; set; } // Primary key
        public int TripBookingModeId { get; set; }



        public int CustomerId { get; set; }

        public int DriverId { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

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


        public String TripBookingModeName { get; set; } = "";
        public required string CustomerName { get; set; }

        public string DriverName { get; set; } = "";



    }
}
