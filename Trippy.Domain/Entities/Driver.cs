using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverId { get; set; } // Primary key

        public string DriverName { get; set; } = "";
        public string License { get; set; } = "";
        public string Nationality { get; set; } = "";

        public string ProfileImagePath { get; set; } = "";  

        public String ContactNumber { get; set; } = "";

        public String NationalId { get; set; } = "";
        public DateTime?  DOB { get; set; }

    
       
        public bool IsRented { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public int CompanyId { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;

    }




}
