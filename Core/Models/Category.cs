using Core.Models.Common;

namespace Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }  // computer, phone, headphone, watch
        public string? ImageUrl { get; set; }

        public bool IsMain { get; set; }


        public ICollection<Category>? Child {  get; set; }
        public Category? Parent { get; set; }
        public int? ParentId { get; set; }

        public ICollection<Product> Products { get; set; }

        //public Category? ParentCategory { get; set; }
        //public int? ParentCategoryId { get; set; }
    }
}
