using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverId { get; set; } // Primary key

        public string Deivername { get; set; } = "";



        public bool IsRented { get; set; } = true;
        public bool IsActive { get; set; } = true;
    }




}
