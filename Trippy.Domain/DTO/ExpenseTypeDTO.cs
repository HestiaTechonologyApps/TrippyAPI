using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class ExpenseTypeDTO
    {
        public int ExpenseTypeId { get; set; } // Primary key

        public String ExpenseTypeName { get; set; } = "";
        public String Description { get; set; } = "";

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public List<AuditLogDTO> AuditLoogs { get; set; } = new List<AuditLogDTO>();
    }
}
