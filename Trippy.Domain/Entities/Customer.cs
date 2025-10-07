using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; } // Primary key

        public required string CustomerName { get; set; }
        public required string CustomerPhone { get; set; }
        public required string CustomerEmail { get; set; }
        public required string CustomerAddress { get; set; }
        public DateTime DOB { get; set; }
        public required string Gender { get; set; }
        public required string Nationalilty { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }

   



}
