using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class InvoiceMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoicemasterId { get; set; }
        public String InvoiceNum { get; set; }
      
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; } = true;

    }




}
