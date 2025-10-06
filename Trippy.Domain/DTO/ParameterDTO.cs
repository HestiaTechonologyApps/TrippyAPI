using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.DTO
{
  
    public class LookUpDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public string Code { get; set; } = "";
        public bool IsSelected { get; set; } = false;
    }
}
