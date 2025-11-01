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

        public String Remark { get; set; } = "";


        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public List<AuditLogDTO> AuditLgs { get; set; } = new List<AuditLogDTO>();
    }
}
