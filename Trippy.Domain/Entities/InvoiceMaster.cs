using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class InvoiceMaster
    {

        public InvoiceMaster()
        { 

            this.InvoiceDetails = new List<InvoiceDetail>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceMasterId { get; set; }
        public String InvoiceNum { get; set; } = "";
      
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; } = true;
        public bool IsCompleted { get; set; } = false;

       public virtual  List<InvoiceDetail> InvoiceDetails { get; set; }

    }


   
}
