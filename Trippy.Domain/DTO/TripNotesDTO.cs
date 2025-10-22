using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
    public class TripNotesDTO
    {
        public int TripNoteId { get; set; } // Primary key


        public string TripNote { get; set; } = "";
        public DateTime CreatedOn { get; set; }
        public List<AuditLogDTO> AuditLogg { get; set; } = new List<AuditLogDTO>();
    }
}
