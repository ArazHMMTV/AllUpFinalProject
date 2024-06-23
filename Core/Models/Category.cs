using Core.Models.Common;

namespace Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; }=null!;



        public Category? Parent { get; set; }
        public int? ParentId { get; set; }

        public ICollection<Category>? Children {  get; set; }= new List<Category>();
        public ICollection<Product>? Products { get; set; }=new List<Product>();

     
    }
}
