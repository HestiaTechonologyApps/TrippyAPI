using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class MonthlyDashboardDTO
    {
        public string Month { get; set; } = "";
        public decimal Expense { get; set; }
        public decimal Invoice { get; set; }
    }
}
