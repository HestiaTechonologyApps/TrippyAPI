using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class TripOrder
    {
        public object AuditLogs;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripOrderId { get; set; } // Primary key
        public int TripBookingModeId { get; set; }

        public string TripCode { get; set; } = "";


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

        public string PaymentMode { get; set; } = "";
        public string PaymentDetails { get; set; } = "";
        public string BookedBy { get; set; } = "";
        public string TripDetails { get; set; } = "";

       

        public string TripStatus { get; set; } = "";// Planned, InProgress, Completed, Cancelled
        public decimal TripAmount { get; set; } = 0;
        public decimal AdvanceAmount { get; set; } = 0;
        public decimal BalanceAmount { get; set; } = 0;
        public bool IsActive { get; set; } = true;


        public bool IsDeleted { get; set; } = false;




    }



   

}
