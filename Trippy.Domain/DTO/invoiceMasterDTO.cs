using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class invoiceMasterDTO
    {
        public int InvoicemasterId { get; set; }
        public String InvoiceNum { get; set; }

        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; } = "";

        public String CreatedBy { get; set; }

        public bool IsDeleted { get; set; } = true;

        public List<AuditLogDTO> AuditLogs { get; set; } = new List<AuditLogDTO>();
    }
}
