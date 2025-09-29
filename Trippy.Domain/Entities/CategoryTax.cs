using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class CategoryTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryTaxId { get; set; } // Primary key

        public String CategoryTaxName { get; set; }
        public String CategoryTaxCode { get; set; }

        public Decimal  CategoryTaxPercentage { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
    }




}
