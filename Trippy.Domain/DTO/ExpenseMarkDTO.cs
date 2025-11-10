using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class ExpenseMarkDTO
    {
        public int ExpenseMasterId { get; set; } // Primary key


        public int ExpenseTypeId { get; set; } // Primary key

        public String ExpenseTypeName { get; set; } = "";
        public decimal Amount { get; set; }
        public String Remark { get; set; } = "";

        public int RelatedEntityId { get; set; } = 0;
        public String RelatedTo { get; set; } = "";
        public String RelatedEntityType { get; set; } = "";
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string CreatedOnString { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
        public int TripOrderId { get; set; }
        public string TripBookingModeName { get; set; } = "";
        public decimal TripAmount { get; set; } = 0;
        public List<AuditLogDTO> AauditLgs { get; set; } = new List<AuditLogDTO>();
    }
}
