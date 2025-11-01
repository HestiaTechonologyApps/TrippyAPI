using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class ExpenseMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseMasterId { get; set; } // Primary key

        public int ExpenseTypeId { get; set; } // Primary key
       
        
        public String Remark { get; set; } = "";


         public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;



    }


}
