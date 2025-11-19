using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class TripNotes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripNoteId { get; set; } // Primary key


        public string TripNote { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;


    }


}
