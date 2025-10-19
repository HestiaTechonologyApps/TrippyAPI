using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Trippy.Domain.Entities
{
    public class AuditLog
    {
        [Key]
        public Guid LogID { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public int? RecordID { get; set; }

        public String? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangeDetails { get; set; }

    }
}
