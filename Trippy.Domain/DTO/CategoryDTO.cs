namespace Trippy.Domain.DTO
{
    public class CategoryDTO
    {
       
        public int CategoryId { get; set; } // Primary key

        public String CategoryName { get; set; }

        public String CategoryDescription { get; set; }
        public String CategoryTitle { get; set; }
        public String CategoryCode { get; set; }
        public String CompanyName { get; set; }
        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; }
        public int CompanyId { get; set; } // Primary key

    }
}
