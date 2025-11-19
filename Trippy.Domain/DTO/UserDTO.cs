using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;

namespace Trippy.Domain.DTO
{
    public class UserDTO
    {

        public int UserId { get; set; } // Primary Key
        public string UserName { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Address { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public bool Islocked { get; set; } = false;
        public int CompanyId { get; set; } =0;
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
        public string CreateAtString { get; set; } = "";
        public DateTime? Lastlogin { get; set; }
        public string LastloginString { get; set; } = "";
        public string CreateAtSyring { get; set; } = "";
        public List<AuditLogDTO> AuditLogs { get; set; } = new List<AuditLogDTO>();
    }
}
