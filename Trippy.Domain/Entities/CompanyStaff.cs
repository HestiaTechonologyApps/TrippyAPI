using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class CompanyStaff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyStaffId { get; set; } // Primary key
        public int CompanyId { get; set; } // Primary key
        public int CompanyBranchId { get; set; } // Primary key
        public String Role { get; set; }
        public bool IsActive { get; set; } = false;
    }




}
