using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Trippy.Domain.Entities
{
    public class CustomerDepartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        
        public String DepartmentName { get; set; }
        public String DepartmentDescription { get; set; }

    }


}
