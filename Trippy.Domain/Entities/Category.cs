using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; } // Primary key

        public String CategoryName { get; set; }

        public String CategoryDescription { get; set; }
        public String CategoryTitle { get; set; }
        public String CategoryCode { get; set; }
        public int CompanyId { get; set; } // Primary key

        public String CategoryImage { get; set; } = "";


        public bool IsDeleted { get; set; } = false;
    }




}
