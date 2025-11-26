using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; } // Primary key

        
        public string Description { get; set; } = "";
        
        public string TableName { get; set; } = "";
        public int? RecordID { get; set; }

        public int ParentCommentId { get; set; }

        public bool IsInternal { get; set; } = false;


        public DateTime? CreatedOn { get; set; }
        public String CreatedBy { get; set; } = "";


        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public String DeletedBy { get; set; } = "";

    }
}
