namespace Trippy.Domain.DTO
{
    public class CustomerDTO
    {

        public int CustomerId { get; set; } // Primary key

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }

        public bool IsActive { get; set; } = true;

        public List<AuditLogDTO> AuditTrails { get; set; }
    }
}
