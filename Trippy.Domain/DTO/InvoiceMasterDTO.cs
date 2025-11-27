using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class InvoiceMasterDTO
    {
        public int InvoicemasterId { get; set; }
        public String InvoiceNum { get; set; }

        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; } = "";

        public String CreatedBy { get; set; }

        public bool IsDeleted { get; set; } = true;

        public List<InvoiceDetailDTO> InvoiceDetailDtos { get; set; } = new List<InvoiceDetailDTO>();
    }

    public class InvoiceDetailDTO
    {
      
        public int InvoiceDetailId { get; set; }
        public int InvoicemasterId { get; set; }

        public int TripOrderId { get; set; }
        public int CategoryId { get; set; } = 0;


        public decimal Ammount { get; set; } = 0;
        public decimal TotalTax { get; set; } = 0;

        public decimal Discount { get; set; } = 0;


    }
    public class InvoiceDetailTaxDTO
    {
        
        public int InvoiceDetailTaxId { get; set; }
        public int InvoiceDetailId { get; set; }

        public int CategoryTaxId { get; set; }

        public Decimal CategoryTaxPercentage { get; set; }

        public Decimal TaxAmount { get; set; }
    }
}
