using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class TripKiloMeter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripKiloMeterId { get; set; } // Primary key

        public int TripOrderId { get; set; } // Primary key
        public int DriverId { get; set; } // Primary key

        public int VehicleId { get; set; }

        public DateTime? TripStartTime { get; set; }
        public DateTime? TripEndTime { get; set; }


        public int TripStartReading { get; set; }
        public int TripEndReading { get; set; }


        public string WaitingHours { get; set; }
        public DateTime CreatedOn { get; set; }


    }


}
