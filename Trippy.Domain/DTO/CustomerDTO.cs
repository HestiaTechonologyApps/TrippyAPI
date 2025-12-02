using Trippy.Domain.Entities;

namespace Trippy.Domain.DTO
{
    public class CustomerDTO
    {

        public int CustomerId { get; set; } // Primary key

        public  string CustomerName { get; set; } = "";
        public  string CustomerPhone { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
        public string CustomerAddress { get; set; }= "";
        public DateTime? DOB { get; set; }
        public string DOBString => DOB.HasValue ? DOB.Value.ToString("yyyy-MM-dd") : "";
        public string Nationality { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public string CreateAtString { get; set; } = "";

        public bool IsActive { get; set; } = true;
        public int CompanyId { get; set; } = 0;
        public string ComapanyName = "";
        public bool IsDeleted { get; set; } = false;
        public  List<AuditLogDTO> AuditTrails { get; set; }
        public ICollection<TripOrder> TripOrders { get; set; } = new List<TripOrder>();
    }
}

