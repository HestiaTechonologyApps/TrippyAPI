using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Entities;

namespace Trippy.Domain.DTO
{
    public class DriverDTO
    {
        public int DriverId { get; set; } // Primary key

        public string DriverName { get; set; } = "";
        public string License { get; set; } = "";
        public string Nationality { get; set; } = "";

        public string ProfileImagePath { get; set; } = "";

        public String ContactNumber { get; set; } = "";
        public String NationalId { get; set; } = "";
        public DateTime? DOB { get; set; }
        public String DOBString { get; set; } = "";
        public bool IsRented { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public List<AuditLogDTO> AuditLog { get; set; } //= new List<AuditLogDTO>();
        public ICollection<TripOrder> TripOrders { get; set; } = new List<TripOrder>();
    }
}
    public class ProfilePicUploadDto
    {
        public int AppUserId { get; set; }
        public IFormFile ProfilePic { get; set; }
    }

