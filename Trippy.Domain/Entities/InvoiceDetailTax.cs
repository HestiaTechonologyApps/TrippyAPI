using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class InvoiceDetailTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceDetailTaxId { get; set; }
        public int InvoiceDetailId { get; set; }

        public int CategoryTaxId { get; set; }

        public Decimal CategoryTaxPercentage { get; set; }

        public Decimal TaxAmount { get; set; }
    }




}
