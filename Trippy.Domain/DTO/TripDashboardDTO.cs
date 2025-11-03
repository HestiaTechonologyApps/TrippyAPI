using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class TripDashboardDTO
    {
        public string Title { get; set; } = "";
        public int Value { get; set; }
        public int Change { get; set; }
        public string Color { get; set; } = "";
        public string Route { get; set; } = "";
        public DateTime? Date { get; set; }
        public string DateString { get; set; } = "";

    }
}
