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

        public string ImageSrc { get; set; } = "";  

        public String ContactNumber { get; set; } = "";

        public DateTime?  DOB { get; set; }       

        public bool IsRented { get; set; } = true;
        public bool IsActive { get; set; } = true;

    }
 



}
