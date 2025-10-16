using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class InvoiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceDetailId { get; set; }
        public int InvoicemasterId { get; set; }

        public int TripOrderId { get; set; }
        public int CategoryId { get; set; } 

         public decimal Ammount { get; set; }
         public decimal TotalTax { get; set; }

        

    }




}
