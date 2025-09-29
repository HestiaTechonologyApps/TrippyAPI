using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class TripBookingMode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripBookingModeId { get; set; } // Primary key
        public String TripBookingModeName { get; set; } = "";
    }




}
