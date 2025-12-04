using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class InvoiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceDetailId { get; set; }
        public int InvoiceMasterId { get; set; }

        public int TripOrderId { get; set; }
        public int CategoryId { get; set; }


        public decimal Ammount { get; set; }
        public decimal TotalTax { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalDiscount { get; set; } = 0;

        public virtual InvoiceMaster InvoiceMaster { get; set; }
        public List<InvoiceDetailTax> InvoiceDetailTaxes { get; set; } = new List<InvoiceDetailTax>();

    }




}
