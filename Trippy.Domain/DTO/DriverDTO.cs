using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class DriverDTO
    {
        public int DriverId { get; set; } // Primary key

        public string DriverName { get; set; } = "";
        public string License { get; set; } = "";
        public string Nationality { get; set; } = "";

        public string ImageSrc { get; set; } = "";

        public String ContactNumber { get; set; } = "";
        public String NationalId { get; set; } = "";
        public DateTime? DOB { get; set; }
        public String DOBString { get; set; } = "";
        public bool IsRented { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public List<AuditLogDTO> AuditLogs { get; set; } = new List<AuditLogDTO>();
    }
}
